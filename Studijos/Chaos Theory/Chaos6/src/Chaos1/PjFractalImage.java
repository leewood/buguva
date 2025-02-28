package Chaos1;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Image;
import java.awt.Point;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.image.MemoryImageSource;
import java.awt.Panel;



import jv.number.PdColor;
import jv.number.PuComplex;
import jv.number.PuInteger;
import jv.object.PsDebug;
import jv.project.PjProject;
import jv.project.PvCameraIf;
import jv.project.PvDisplayIf;
import jv.project.PvPickEvent;
import jv.project.PvViewerIf;
import jv.vecmath.PdVector;
import java.util.ArrayList;
import jv.vecmath.PiVector;

public class PjFractalImage extends PjProject implements ComponentListener {
	/** Color type. */
	protected	final static int	COLOR_BLACK			= 1;
	/** Color type. */
	protected	final static int	COLOR_REDBLACK		= 2;
	/** Color type. */
	protected	final static int	COLOR_HUE			= 3;
	/** Color type. */
	protected	final static int	COLOR_HUEOFFSET	= 4;
	
	/** Display of main window. */
	protected	PvDisplayIf			m_dispJulia;
    protected	PvDisplayIf			m_dispNiuton;
	/** Image to be used as background in display. */
	protected	Image					m_image;
	/** Height of display canvas. */
	protected	int					m_imageHeight;
	/** Width of display canvas. */
	protected	int					m_imageWidth;
	/** Producer of image m_image from pixel array. */
	private		MemoryImageSource m_mis;
	/** Pixel array which stores the image of a textured element. Only used when texture enabled. */
	private		PiVector				m_pix;
	/** Clone of pixel array to avoid re-initialization in tracking paths. */
	private		PiVector				m_pixStore;
	/** Pixel array which stores known used iterations. */
	private		PiVector				m_pixIter;
	/** Rectangular section in the complex plane where Julia set is shown. */
	protected	PdVector				m_bounds;

	/** Trace of touched pixels during an iteration. */
	private		PiVector				m_pixTrace;
	/** Array of colors for each number of iteration, size of array is maxIter. */
	private		PiVector				m_colMap;
	/** Maximal number of iterations until sequence is decided to diverge. */
	protected	PuInteger			m_maxIter;
	/**
	 * Determines size of uniformly colored pixels blocks in images.
	 * Using discr==1 leads to highest resolution where each image pixel is really computed.
	 */
	protected	PuInteger			m_blockSize;
	/** Constant of Julia set. */
	protected	PuComplex			m_const;
	/** Offset of hue coloring. */
	protected	PuInteger			m_hueOffset;
	
	
	/** Display of module space. */
	protected	PvDisplayIf			m_dispMandelbrot;
	/** Image to be used as background in display. */
	protected	Image					m_imageMandelbrot;
	/** Height of display canvas. */
	protected	int					m_imageHeightMandelbrot;
	/** Width of display canvas. */
	protected	int					m_imageWidthMandelbrot;
	/** Producer of image m_imageMandelbrot from pixel array. */
	private		MemoryImageSource m_misMandelbrot;
	/** Pixel array which stores the image of a textured element. Only used when texture enabled. */
	private		PiVector				m_pixMandelbrot;
	/** Clone of pixel array to avoid re-initialization in tracking paths. */
	private		PiVector				m_pixStoreMandelbrot;
	/** Pixel array which stores known used iterations. */
	private		PiVector				m_pixIterMandelbrot;
	/** Rectangular section in the complex plane where Mandelbrot set is shown. */
	protected	PdVector				m_boundsMandelbrot;
    protected	PdVector				m_boundsNiuton;
    private		PiVector				m_pixIterNiuton;
    private		PiVector				m_pixStoreNiuton;
    private		PiVector				m_pixNiuton;
    private		MemoryImageSource m_misNiuton;
	protected	Image					m_imageNiuton;
	protected	int					m_imageHeightNiuton;
	/** Width of display canvas. */
	protected	int					m_imageWidthNiuton;
    public PaintingArea area = null;

    private boolean paintNiuton = false;

    public void setPaintNiuton(boolean value)
    {
        paintNiuton = value;
    }

	public PjFractalImage() {
		super("Julijos aibės peržiūra");
		
		m_pix						= new PiVector();
		m_pixIter				= new PiVector();
		m_pixStore				= new PiVector();


		m_pixNiuton						= new PiVector();
		m_pixIterNiuton				= new PiVector();
		m_pixStoreNiuton				= new PiVector();


		m_pixMandelbrot		= new PiVector();
		m_pixIterMandelbrot	= new PiVector();
		m_pixStoreMandelbrot	= new PiVector();
		
		m_pixTrace				= new PiVector();
		m_colMap					= new PiVector();
		
		m_bounds					= new PdVector(-2.0, -1.5, 2.0, 1.5);
		m_boundsMandelbrot	= new PdVector(-2, -1.5, 2, 1.5);
		m_boundsNiuton					= new PdVector(-2.5, -2.5, 2.5, 2.5);
		m_maxIter				= new PuInteger("Žingsnių skaičius", this);
		m_blockSize				= new PuInteger("Bloko dydis", this);
		m_hueOffset				= new PuInteger("Spalvingumas", this);
		
		if (getClass() == PjFractalImage.class)
			init();
	}
	public void init() {
		super.init();
		m_image					= null;
		m_imageMandelbrot		= null;
		m_imageNiuton = null;
		m_pix.setSize(0);
		m_pixIter.setSize(0);
		m_pixStore.setSize(0);

		m_pixMandelbrot.setSize(0);
		m_pixIterMandelbrot.setSize(0);
		m_pixStoreMandelbrot.setSize(0);

        m_pixNiuton.setSize(0);
        m_pixIterNiuton.setSize(0);
        m_pixStoreNiuton.setSize(0);

		m_imageHeight			= 0;
		m_imageWidth			= 0;
		m_imageHeightMandelbrot	= 0;
		m_imageWidthMandelbrot	= 0;
		m_imageWidthNiuton = 0;
        m_imageHeightNiuton = 0;

		m_maxIter.setDefBounds(1, 200, 1, 5);
		m_maxIter.setDefValue(40);
		m_maxIter.init();
		m_colMap.setSize(m_maxIter.getValue()+1);
		m_pixTrace.setSize(m_maxIter.getValue()+1);
		
		m_blockSize.setDefBounds(1, 10, 1, 2);
		m_blockSize.setDefValue(1);
		m_blockSize.init();
		
		m_hueOffset.setDefBounds(0, 255, 1, 5);
		m_hueOffset.setDefValue(0);
		m_hueOffset.init();
		
		m_const = new PuComplex(0., 0.75);
	}
	/**
	 * Called when project is launched by viewer on applet start.
	 */
	public void start() {
		if (m_dispJulia == null) {
			m_dispJulia		= getDispJulia();
		}
		if (m_dispMandelbrot == null) {
			m_dispMandelbrot	= getDispMandelbrot();
		}

        if (m_dispNiuton == null)
        {
            m_dispNiuton = getDispNiuton();
        }

		// Determine rectangular section in the complex plane
		// where the Julia and Mandelbrot sets are computed.
		m_bounds.set(-2.0, -1.5, 2.0, 1.5);
        if (mode == 0)
        {
		   m_boundsMandelbrot.set(-2, -1.5, 2, 1.5);
        }
        else
        {
            m_boundsMandelbrot.set(-2, -1.5, 2, 1.5);
        }
		m_boundsNiuton.set(-2.5, -2.5, 2.5, 2.5);
		// Adjust sizes of images to dimension of display canvas
		if (resizeImage(m_dispJulia)) {
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
		}

        if (resizeImage(m_dispNiuton)) {
            if (paintNiuton)
            {
			  computeImageNiuton(m_dispNiuton,
										  m_boundsNiuton.m_data[0], m_boundsNiuton.m_data[1],
										  m_boundsNiuton.m_data[2], m_boundsNiuton.m_data[3]);
			  m_dispNiuton.update(null);
            }
        }

		if (resizeImage(m_dispMandelbrot)) {
			computeImageMandelbrot(m_dispMandelbrot,
										  m_boundsMandelbrot.m_data[0], m_boundsMandelbrot.m_data[1],
										  m_boundsMandelbrot.m_data[2], m_boundsMandelbrot.m_data[3]);
			m_dispMandelbrot.update(null);
		}

		if (m_dispJulia != null) {
			m_dispJulia.selectCamera(PvCameraIf.CAMERA_ORTHO_XY);
			m_dispJulia.setBackgroundImageFit(PvDisplayIf.IMAGE_RESIZE);
			m_dispJulia.setMajorMode(PvDisplayIf.MODE_INITIAL_PICK);
            try
            {
               m_dispJulia.showBackgroundImage(true);               
            } catch (Throwable e)
            {
                
            }
		}

        if (m_dispNiuton != null) {
            if (paintNiuton)
            {
            
			  m_dispNiuton.selectCamera(PvCameraIf.CAMERA_ORTHO_XY);
			  m_dispNiuton.setBackgroundImageFit(PvDisplayIf.IMAGE_RESIZE);
			  m_dispNiuton.setMajorMode(PvDisplayIf.MODE_INITIAL_PICK);
              try
              {
			     m_dispNiuton.showBackgroundImage(true);
              } catch (Throwable e)
              {
              }
            }
		}

		if (m_dispMandelbrot != null) {
			m_dispMandelbrot.selectCamera(PvCameraIf.CAMERA_ORTHO_XY);
			m_dispMandelbrot.setBackgroundImageFit(PvDisplayIf.IMAGE_RESIZE);
			m_dispMandelbrot.setMajorMode(PvDisplayIf.MODE_INITIAL_PICK);
            try
            {
			m_dispMandelbrot.showBackgroundImage(true);
            } catch (Throwable e)
            {
            }
            
		}
		update(this);
		super.start();
	}
	/**
	 * Update the class whenever a child has changed.
	 * Method is usually invoked from the children.
	 */
	public boolean update(Object event) {
		if (m_dispJulia == null)
			return super.update(event);

		if (event == this) {
			return super.update(this);
		} else if (event == m_maxIter) {
			m_colMap.setSize(m_maxIter.getValue()+1);
			m_pixTrace.setSize(m_maxIter.getValue()+1);
			
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
			
			computeImageMandelbrot(m_dispMandelbrot,
										  m_boundsMandelbrot.m_data[0], m_boundsMandelbrot.m_data[1],
										  m_boundsMandelbrot.m_data[2], m_boundsMandelbrot.m_data[3]);
			m_dispMandelbrot.update(null);
            if (paintNiuton)
            {

			  computeImageNiuton(m_dispNiuton,
									m_boundsNiuton.m_data[0], m_boundsNiuton.m_data[1], m_boundsNiuton.m_data[2], m_boundsNiuton.m_data[3]);
    			m_dispNiuton.update(null);
            }
			return true;
		} else if (event == m_blockSize) {
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
			return true;
		} else if (event == m_const) {
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
            if (paintNiuton)
            {

    			computeImageNiuton(m_dispNiuton,
  									m_boundsNiuton.m_data[0], m_boundsNiuton.m_data[1], m_boundsNiuton.m_data[2], m_boundsNiuton.m_data[3]);
	    		m_dispNiuton.update(null);
            }
			return super.update(this);
		} else if (event == m_hueOffset) {
			// For speed: do not recompute the iteration, only assign color values again.
			//	computeImageJulia(m_dispJulia,
			//					 m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			computeColors(m_pix.m_data, m_pixIter.m_data, m_imageWidth*m_imageHeight, m_maxIter.getValue()+1,
							  COLOR_HUEOFFSET);
			// Store current version.
			m_pixStore.copy(m_pix.m_data, m_imageWidth*m_imageHeight);
			// Update the image with the newly computed pixels
			m_mis.newPixels(0, 0, m_imageWidth, m_imageHeight);
			m_dispJulia.update(null);
			return true;
		}
		return super.update(event);
	}
	/**
	 * Adjust sizes of two images to dimension of Julia display canvas.
	 */
	private boolean resizeImage(PvDisplayIf disp) {
		if (disp == null)
			return false;
		boolean bResized	= false;
		if (disp == m_dispJulia) {
			Dimension dim = m_dispJulia.getSize();
			if (dim.height>0 && dim.width>0 &&
				 (m_mis==null || m_imageHeight!=dim.height || m_imageWidth!=dim.width)) {
				m_imageHeight			= dim.height;
				m_imageWidth			= dim.width;
				m_pix.setSize(m_imageWidth*m_imageHeight);
				m_pixIter.setSize(m_imageWidth*m_imageHeight);
				m_mis = new MemoryImageSource(m_imageWidth, m_imageHeight, m_pix.m_data, 0, m_imageWidth);
				// Enables the possibility to update the pixels in m_pix
				m_mis.setAnimated(true);
				// Create image which will be painted as background in PvDisplay.
				m_image = ((Component)m_dispJulia).createImage(m_mis);
				m_dispJulia.setBackgroundImage((Image)m_image);
				m_dispJulia.update(null);
				bResized	= true;
			}
			return bResized;
		} else if (disp == m_dispMandelbrot) {
			Dimension dim = m_dispMandelbrot.getSize();
			if (dim.height>0 && dim.width>0 &&
				 (m_misMandelbrot==null || m_imageHeightMandelbrot!=dim.height || m_imageWidthMandelbrot!=dim.width)) {
				m_imageHeightMandelbrot	= dim.height;
				m_imageWidthMandelbrot	= dim.width;
				m_pixMandelbrot.setSize(m_imageWidthMandelbrot*m_imageHeightMandelbrot);
				m_pixIterMandelbrot.setSize(m_imageWidthMandelbrot*m_imageHeightMandelbrot);
				m_misMandelbrot = new MemoryImageSource(m_imageWidthMandelbrot, m_imageHeightMandelbrot, m_pixMandelbrot.m_data, 0, m_imageWidthMandelbrot);
				// Enables the possibility to update the pixels in m_pix
				m_misMandelbrot.setAnimated(true);
				// Create image which will be painted as background in PvDisplay.
				m_imageMandelbrot = ((Component)m_dispMandelbrot).createImage(m_misMandelbrot);
				m_dispMandelbrot.setBackgroundImage((Image)m_imageMandelbrot);
				m_dispMandelbrot.update(null);
				bResized	= true;
			}
            return bResized;
        } else if (disp == m_dispNiuton) {
     			Dimension dim = m_dispNiuton.getSize();
			if (dim.height>0 && dim.width>0 &&
				 (m_misNiuton==null || m_imageHeightNiuton!=dim.height || m_imageWidthNiuton!=dim.width)) {
				m_imageHeightNiuton	= dim.height;
				m_imageWidthNiuton	= dim.width;
				m_pixNiuton.setSize(m_imageWidthNiuton*m_imageHeightNiuton);
				m_pixIterNiuton.setSize(m_imageWidthNiuton*m_imageHeightNiuton);
				m_misNiuton = new MemoryImageSource(m_imageWidthNiuton, m_imageHeightNiuton, m_pixNiuton.m_data, 0, m_imageWidthNiuton);
				// Enables the possibility to update the pixels in m_pix
				m_misNiuton.setAnimated(true);
				// Create image which will be painted as background in PvDisplay.
				m_imageNiuton = ((Component)m_dispNiuton).createImage(m_misNiuton);
				m_dispNiuton.setBackgroundImage((Image)m_imageNiuton);
				m_dispNiuton.update(null);
				bResized	= true;
            
            }

			return bResized;
		}

		return false;
	}
	/** Invoked when component has been shown. */
	public void componentShown(ComponentEvent comp) {}
	/** Invoked when component has been hidden. */
	public void componentHidden(ComponentEvent comp) {}
	/** Invoked when component has been moved. */
	public void componentMoved(ComponentEvent comp) {}
	/** When component has been resized all images must be resized. */
	public void componentResized(ComponentEvent comp) {
		// Adjust sizes of images to dimension of display canvas
		Object source = comp.getSource();
		if (source == m_dispJulia) {
			if (!resizeImage(m_dispJulia))
				return;
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
		} else if (source == m_dispMandelbrot) {
			if (!resizeImage(m_dispMandelbrot))
				return;
			computeImageMandelbrot(m_dispMandelbrot,
										  m_boundsMandelbrot.m_data[0], m_boundsMandelbrot.m_data[1],
										  m_boundsMandelbrot.m_data[2], m_boundsMandelbrot.m_data[3]);
			m_dispMandelbrot.update(null);
        }
        
        else if (source == m_dispNiuton) {
            if (paintNiuton)
            {

  			  if (!resizeImage(m_dispNiuton))
  			  	  return;
			  computeImageNiuton(m_dispNiuton,
										  m_boundsNiuton.m_data[0], m_boundsNiuton.m_data[1],
										  m_boundsNiuton.m_data[2], m_boundsNiuton.m_data[3]);
			  m_dispNiuton.update(null);
            }
        }
         
	}
	/**
	 * Get display of Julia set. The shown Julia set is determined
	 * by a complex value c to be picked in the Mandelbrot image.
	 * Picking inside the Julia set will show the iterated values
	 * of the picked point z under the map [z -> z^2+c].
	 */

	public PvDisplayIf getDispNiuton() {
        

        if (m_dispNiuton != null)
			return m_dispNiuton;

		if (getDisplay() != null) {
			// If available then use default project display.
			m_dispNiuton = getDisplay();
		} else {
			// Get viewer and ask for another, new display
			PvViewerIf viewer = getViewer();
			// Create right window and add a clone of the torus geometry.
			m_dispNiuton = viewer.newDisplay("Niuton Set", false);
		}
		m_dispNiuton.setBackgroundColor(Color.white);
		m_dispNiuton.addPickListener(this);
		((Component)m_dispNiuton).addComponentListener(this);
        
		return m_dispNiuton;
	}


	public PvDisplayIf getDispJulia() {
		if (m_dispJulia != null)
			return m_dispJulia;
		
		if (getDisplay() != null) {
			// If available then use default project display.
			m_dispJulia = getDisplay();
		} else {
			// Get viewer and ask for another, new display
			PvViewerIf viewer = getViewer();
			// Create right window and add a clone of the torus geometry.
			m_dispJulia = viewer.newDisplay("Julia Set", false);
		}
		m_dispJulia.setBackgroundColor(Color.white);
		m_dispJulia.addPickListener(this);
		((Component)m_dispJulia).addComponentListener(this);
		return m_dispJulia;
	}
	/**
	 * Get display of Mandelbrot set which is used to select the complex parameter
	 * value c which determines the Julia set.
	 */
	public PvDisplayIf getDispMandelbrot() {
		if (m_dispMandelbrot != null)
			return m_dispMandelbrot;

		// Get viewer and ask for another display
		PvViewerIf viewer = getViewer();
		// Create left window and add a clone of the torus geometry.
		m_dispMandelbrot = viewer.newDisplay("Mandelbrot Space", false);
		m_dispMandelbrot.setBackgroundColor(new Color(255, 255, 150));
		m_dispMandelbrot.addPickListener(this);
		((Component)m_dispMandelbrot).addComponentListener(this);
		return m_dispMandelbrot;
	}
	/**
	 * Method is called from display when user drags a rectangular array.
	 * The pixel rectangle is converted into a rectangle in the complex plane
	 * and used to zoom into the Julia or Mandelbrot set.
	 * @param		pickEvent		Everything in a 
	 * 									{@link jv.project.PvPickEvent#getMarkBox() PvPickEvent MarkBox}
	 * 									gets marked. 
	 */
	public void markVertices(PvPickEvent pickEvent) {
		if (pickEvent.getSource() == m_dispJulia) {
			PiVector pickBox	= pickEvent.getMarkBox();
			int [] markBox		= pickBox.m_data;
			double domWidth	= m_bounds.m_data[2]-m_bounds.m_data[0];
			double domHeight	= m_bounds.m_data[3]-m_bounds.m_data[1];
			
			double xMinNew		= m_bounds.m_data[0] + domWidth*markBox[0]/(m_imageWidth-1.);
			double xMaxNew		= m_bounds.m_data[0] + domWidth*markBox[2]/(m_imageWidth-1.);
			double yMinNew		= m_bounds.m_data[1] + domHeight*markBox[1]/(m_imageHeight-1.);
			double yMaxNew		= m_bounds.m_data[1] + domHeight*markBox[3]/(m_imageHeight-1.);
			computeImageJulia(m_dispJulia, xMinNew, yMinNew, xMaxNew, yMaxNew);
			m_dispJulia.update(null);
		} else if (pickEvent.getSource() == m_dispMandelbrot) {
			PiVector pickBox	= pickEvent.getMarkBox();
			int [] markBox		= pickBox.m_data;
			double domWidth	= m_boundsMandelbrot.m_data[2]-m_boundsMandelbrot.m_data[0];
			double domHeight	= m_boundsMandelbrot.m_data[3]-m_boundsMandelbrot.m_data[1];
			
			double xMinNew		= m_boundsMandelbrot.m_data[0] + domWidth*markBox[0]/(m_imageWidthMandelbrot-1.);
			double xMaxNew		= m_boundsMandelbrot.m_data[0] + domWidth*markBox[2]/(m_imageWidthMandelbrot-1.);
			double yMinNew		= m_boundsMandelbrot.m_data[1] + domHeight*markBox[1]/(m_imageHeightMandelbrot-1.);
			double yMaxNew		= m_boundsMandelbrot.m_data[1] + domHeight*markBox[3]/(m_imageHeightMandelbrot-1.);
			computeImageMandelbrot(m_dispMandelbrot, xMinNew, yMinNew, xMaxNew, yMaxNew);
			m_dispMandelbrot.update(null);
		}
        else if (pickEvent.getSource() == m_dispNiuton) {
            if (paintNiuton)
            {
			   PiVector pickBox	= pickEvent.getMarkBox();
			   int [] markBox		= pickBox.m_data;
			   double domWidth	= m_boundsNiuton.m_data[2]-m_boundsNiuton.m_data[0];
			   double domHeight	= m_boundsNiuton.m_data[3]-m_boundsNiuton.m_data[1];

   			   double xMinNew		= m_boundsNiuton.m_data[0] + domWidth*markBox[0]/(m_imageWidthNiuton-1.);
			   double xMaxNew		= m_boundsNiuton.m_data[0] + domWidth*markBox[2]/(m_imageWidthNiuton-1.);
			   double yMinNew		= m_boundsNiuton.m_data[1] + domHeight*markBox[1]/(m_imageHeightNiuton-1.);
			   double yMaxNew		= m_boundsNiuton.m_data[1] + domHeight*markBox[3]/(m_imageHeightNiuton-1.);
			   computeImageNiuton(m_dispNiuton, xMinNew, yMinNew, xMaxNew, yMaxNew);
			   m_dispNiuton.update(null);
            }
		
        }
        update(this);
	}
	/**
	 * Method is called from display when a user picks into the display
	 * in initial-pick mode. This method iterates the picked point
	 * using the Julia or Mandelbrot function and displays the iterated
	 * points.
	 * @param		pickEvent		Pick event issued by the display on left mouse pick.
	 * @see			jv.project.PvPickListenerIf
	 */
	public void pickInitial(PvPickEvent pickEvent) {
		if (pickEvent.getSource() == m_dispJulia) {
			double xMin		= m_bounds.m_data[0];
			double xMax		= m_bounds.m_data[2];
			double yMin		= m_bounds.m_data[1];
			double yMax		= m_bounds.m_data[3];
			double width	= xMax - xMin;
			double height	= yMax - yMin;
			
			Point loc		= pickEvent.getLocation();

			double u			= xMin + width*loc.x/(m_imageWidth-1.);
			double v			= yMin + height*loc.y/(m_imageHeight-1.);

			// Repaint current images.
			m_pix.copyArray(m_pixStore);

			// This is iteration f(z)=z*z+c with c=z0.
			// The picked position z0 and its iterated positions are displayed.
			
			PuComplex c		= new PuComplex();
			PuComplex z		= new PuComplex(u, v);
			/*
			// In case we want to trace the point z in the Mandelbrot set.
			switch (m_type) {
			default:
			case MANDELBROT:
			c.copy(z);
			break;
			case JULIA:
			c.copy(m_const);
			break;
			}
			*/
			c.copy(m_const);
			
			int maxIter		= 20; // m_maxIter.getValue();
			for (int k=0; k<maxIter; k++) {
				// Draw pixel
				int ind	= getImagePosition(z.re, z.im);
				if (ind != -1) {
					int col	= PdColor.hsv2rgbAsInt(0, 255*k/maxIter, 255);
					m_pix.m_data[ind]	= col;
					if (ind+m_imageWidth+1 < m_imageWidth*m_imageHeight) {
						m_pix.m_data[ind+1]	= col;
						m_pix.m_data[ind+m_imageWidth]	= col;
						m_pix.m_data[ind+m_imageWidth+1]	= col;
					}
				}
				
				z.sqr().add(c);
				// If sequence diverges then note the used number of iterations and break.
				if (z.sqrAbs() > 4.) {
					break;
				}
				if (z.re<xMin || z.re>xMax || z.im<yMin || z.im>yMax)
					continue;
			}

			// Update the image with the newly computed pixels
			m_mis.newPixels(0, 0, m_imageWidth, m_imageHeight);
			m_dispJulia.update(null);
		} else if (pickEvent.getSource() == m_dispMandelbrot) {
			double xMin		= m_boundsMandelbrot.m_data[0];
			double xMax		= m_boundsMandelbrot.m_data[2];
			double yMin		= m_boundsMandelbrot.m_data[1];
			double yMax		= m_boundsMandelbrot.m_data[3];
			double width	= xMax - xMin;
			double height	= yMax - yMin;
			
			Point loc		= pickEvent.getLocation();

			double u			= xMin + width*loc.x/(m_imageWidthMandelbrot-1.);
			double v			= yMin + height*loc.y/(m_imageHeightMandelbrot-1.);
			m_const.set(u,v);

			// Recompute Julia set with new parameter m_const.
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
            if (paintNiuton)
            {
               computeImageNiuton(m_dispNiuton, m_boundsNiuton.m_data[0], m_boundsNiuton.m_data[1], m_boundsNiuton.m_data[2], m_boundsNiuton.m_data[3]);
			  m_dispNiuton.update(null);
            }
            area.initTransformations(m_const);
            area.untilStep = 12;
            area.repaint();

		}
		update(this);
	}
	/**
	 * Converts a point (u,v) in the complex plane to its position in the image of the Julia set.
	 * @param		u		real value of complex position
	 * @param		v		imaginary value of complex position
	 * @return		index in the pixel array
	 */
	private int getImagePosition(double u, double v) {
		double xMin			= m_bounds.m_data[0];
		double xMax			= m_bounds.m_data[2];
		double yMin			= m_bounds.m_data[1];
		double yMax			= m_bounds.m_data[3];
		
		if (u<xMin || u>xMax || v<yMin || v>yMax)
			return -1;
		
		double domWidth	= xMax - xMin;
		double domHeight	= yMax - yMin;

		// Note, adding 0.5 is already incorporated into initial value of u and v in computeJulia.
		int ix	= (int)((m_imageWidth-1)*(u-xMin)/domWidth + 0.5);
		int iy	= (int)((m_imageHeight-1)*(v-yMin)/domHeight + 0.5);
		int ind	= iy*m_imageWidth+ix;
		if (ind<0 || ind>=m_pix.m_data.length) // m_imageWidth*m_imageHeight)
			return -1;
		return ind;
	}
	/**
	 * Compute a Julia set in the given rectangle in the complex plane
	 * and updates the corresponding images.
	 * <p>
	 * Parameters determine a rectangle in the complex plane in which
	 * the Julia set is computed.
	 */
	public void computeImageJulia(PvDisplayIf disp, double xMin, double yMin, double xMax, double yMax) {
		m_bounds.set(xMin, yMin, xMax, yMax);
		// Paint in pixel array m_pix.
		int maxIter		= m_maxIter.getValue();
		int blockSize	= m_blockSize.getValue();
		
		computeJulia(m_pixIter.m_data, m_imageWidth, m_imageHeight, blockSize,
						 xMin, yMin, xMax, yMax, maxIter,
						 m_const);
		computeColors(m_pix.m_data, m_pixIter.m_data, m_imageWidth*m_imageHeight, maxIter+1,
						  COLOR_HUEOFFSET);
		// Store current version.
		m_pixStore.copy(m_pix.m_data, m_imageWidth*m_imageHeight);
		// Update the image with the newly computed pixels
		m_mis.newPixels(0, 0, m_imageWidth, m_imageHeight);
	}
	/**
	 * Compute a Mandelbrot set in the given rectangle in the complex plane
	 * and updates the corresponding images.
	 * <p>
	 * Parameters determine a rectangle in the complex plane in which
	 * the Mandelbrot set is computed.
	 */

    private int mode = 0;

    public void setMode(boolean value)
    {
        if (value)
        {
            mode = 1;
        }
        else
        {
            mode = 2;
        }
    }

	public void computeImageMandelbrot(PvDisplayIf disp, double xMin, double yMin, double xMax, double yMax) {
		m_boundsMandelbrot.set(xMin, yMin, xMax, yMax);
		// Paint in pixel array m_pix.
		int maxIter		= m_maxIter.getValue();
		int blockSize	= m_blockSize.getValue();
		
		computeMandelbrot(m_pixIterMandelbrot.m_data, m_imageWidthMandelbrot, m_imageHeightMandelbrot, blockSize,
								xMin, yMin, xMax, yMax, maxIter);
		computeColors(m_pixMandelbrot.m_data, m_pixIterMandelbrot.m_data, m_imageWidthMandelbrot*m_imageHeightMandelbrot, maxIter+1,
						  COLOR_HUE);
		// COLOR_BLACK);
		// Store current version.
		m_pixStoreMandelbrot.copy(m_pixMandelbrot.m_data, m_imageWidthMandelbrot*m_imageHeightMandelbrot);
		// Update the image with the newly computed pixels
		m_misMandelbrot.newPixels(0, 0, m_imageWidthMandelbrot, m_imageHeightMandelbrot);
	}

	public void computeImageNiuton(PvDisplayIf disp, double xMin, double yMin, double xMax, double yMax) {
		
        m_boundsNiuton.set(xMin, yMin, xMax, yMax);
		// Paint in pixel array m_pix.
		int maxIter		= m_maxIter.getValue();
		int blockSize	= m_blockSize.getValue();

		computeNiuton(m_pixIterNiuton.m_data, m_imageWidthNiuton, m_imageHeightNiuton, blockSize,
								xMin, yMin, xMax, yMax, maxIter,m_const);
		computeColors(m_pixNiuton.m_data, m_pixIterNiuton.m_data, m_imageWidthNiuton*m_imageHeightNiuton, maxIter+1,
						  COLOR_HUE);
		// COLOR_BLACK);
		// Store current version.
		m_pixStoreNiuton.copy(m_pixNiuton.m_data, m_imageWidthNiuton*m_imageHeightNiuton);
		// Update the image with the newly computed pixels
		m_misNiuton.newPixels(0, 0, m_imageWidthNiuton, m_imageHeightNiuton);
         
	}
	/**
	 * Compute a Julia set in the given rectangle in the complex plane.
	 * Method returns a pixel array with given width and height.
	 * Adjusting the maximal number of iterations and blockSize allows to speed up
	 * the calculation at the cost of accuracy.
	 * <p>
	 * The algorithm determines for given complex parameter c if the iteration f(z)=z*z+c
	 * diverges to infinity which is assumed if |z|>4.
	 * <p>
	 * Parameters determine a rectangle in the complex plane in which
	 * the Julia set is computed.
	 * 
	 * @param		pixIter		pixel image with RGB integers which will be computed.
	 * @version		10.08.05, 1.00 revised (kp) Major revision and optimization.<br>
	 */
	public void computeJulia(int [] pixIter, int imageWidth, int imageHeight, int blockSize,
									 double xMin, double yMin, double xMax, double yMax, int maxIter,
									 PuComplex c) {
		int col, blockRemain;
		PuComplex z	= new PuComplex();
		double du	= blockSize*(xMax-xMin)/(imageWidth-1.);
		double dv	= blockSize*(yMax-yMin)/(imageHeight-1.);
		// PsDebug.initTime();
		double v		= yMin;
		int ind		= 0;
		for (int i=0; i<imageHeight; i+=blockSize) {
			double u		= xMin;
			int indPrev = ind;
			for (int j=0; j<imageWidth; j+=blockSize) {
				z.set(u, v);
				
				// This is Julia iteration f(z)=z*z+c with fixed c.
				// For each z check whether iteration diverges to infinity
				int numIter	= 0;
				for (int k=0; k<maxIter; k++) {
                    if (mode == 0)
                    {
					  z.sqr().add(c);
                    } else {
                      z = PuComplex.pow(z, 7).add(PuComplex.pow(z, 5).neg()).add(c);
                    }
					// If sequence diverges then note the used number of iterations and break.
					if (z.sqrAbs() > 4.) {
						numIter		= k+1;
						break;
					}
				}
				// Store the used number of iterations, and duplicate within block
				blockRemain = Math.min(blockSize, imageWidth-j);
				for (int k=0; k<blockRemain; k++)
					pixIter[ind++] = numIter;

				u += du;
			}
			// Duplicate current line to fill blocks. Only effective if blockSize>1.
			blockRemain = Math.min(blockSize, imageHeight-i)-1;
			for (int k=0; k<blockRemain; k++) {
				System.arraycopy(pixIter, indPrev, pixIter, ind, imageWidth);
				ind += imageWidth;
			}
			v += dv;
		}
		
		// PsDebug.message("Seconds per frame = "+PsDebug.getTimeUsed());
	}
	/**
	 * Compute a Mandelbrot set in the given rectangle in the complex plane.
	 * Method returns a pixel array with given width and height.
	 * Adjusting the maximal number of iterations and blockSize allows to speed up
	 * the calculation at the cost of accuracy.
	 * <p>
	 * The algorithm determines for each z0 if the iteration f(z)=z*z+z0
	 * diverges to infinity which is assumed if |z|>4.
	 * <p>
	 * Parameters determine a rectangle in the complex plane in which
	 * the Mandelbrot set is computed.
	 * 
	 * @param		pixIter		pixel image with RGB integers which will be computed.
	 * @version		10.08.05, 1.00 revised (kp) Major revision and optimization.<br>
	 */
    ArrayList<PuComplex> iter1 = new ArrayList();
    ArrayList<PuComplex> iter2 = new ArrayList();
    ArrayList<PuComplex> current = new ArrayList();

    public double dia(PuComplex pu1, PuComplex pu2)
    {
        return Math.sqrt(Math.pow(PuComplex.re(pu1) - PuComplex.re(pu2), 2) + Math.pow(PuComplex.im(pu1) - PuComplex.im(pu2), 2));
    }

    public double calc()
    {
        double min = dia(iter1.get(0), iter2.get(0));
        for (int i = 1; i < iter1.size(); i++)
        {
            PuComplex pu1 = iter1.get(i);
            for (int j = 0; j < iter2.size(); j++)
            {
                PuComplex pu2 = iter2.get(j);
                double di = dia(pu1, pu2);
                if (di < min)
                {
                    min = di;

                }
            }
        }
        return min;
    }

    public void iterate(ArrayList<PuComplex> current, PuComplex c)
    {
        iter1.clear();
        iter2.clear();
        for (int i = 0; i < current.size(); i++)
        {
            PuComplex cur = current.get(i);
            PuComplex z1 = new PuComplex();
            PuComplex z2 = new PuComplex();
            z2.set(cur.re, cur.im);
            z1.set(cur.re, cur.im);
            z1 = z1.mult(c).add(-1);
            z2 = z2.mult(c).add(1);
            iter1.add(z1);
            iter2.add(z2);
        }
    }

    public double checkWith(PuComplex c, int iter)
    {
        return Math.pow(c.sqrAbs(), iter + 1) * 2 * 1.5;
    }

    public void union()
    {
        current.clear();
        for (int i = 0; i < iter1.size();i++)
        {
            if (!current.contains(iter1.get(i)))
            {
                current.add(iter1.get(i));
            }
            if (!current.contains(iter2.get(i)))
            {
                current.add(iter2.get(i));
            }
        }
    }

	public void computeMandelbrot(int [] pixIter, int imageWidth, int imageHeight, int blockSize,
											double xMin, double yMin, double xMax, double yMax, int maxIter) {
		PuComplex z	= new PuComplex();
		PuComplex c	= new PuComplex();
        PuComplex z2 = new PuComplex();
        PuComplex c2 = new PuComplex();
		double du	= blockSize*(xMax-xMin)/(imageWidth-1.);
		double dv	= blockSize*(yMax-yMin)/(imageHeight-1.);
        maxIter = maxIter / 10;
		// PsDebug.initTime();
		int ind		= 0;
		double v		= yMin;
		for (int i=0; i<imageHeight; i+=blockSize) {
			int indPrev = ind;
			double u		= xMin;
			for (int j=0; j<imageWidth; j+=blockSize) {
				c.set(u, v);
				z.set(u, v);
				c2.set(u, v);
                z2.set(u, v);
                current.clear();
                current.add(new PuComplex(0,0));                				// This is Mandelbrot iteration f(z)=z*z+c with c=z0.
				// For each z check whether iteration diverges to infinity
				int numIter	= 0;

				for (int k=0; k<maxIter; k++) {
					//
                      iterate(current, c);
                      

					// If sequence diverges then note the used number of iterations and break.
					if (calc() <= checkWith(c, k + 1)) {
						numIter		= k+1;
						break;
					}
                      union();
				}
				// Store the used number of iterations, and duplicate within block
				int blockRemain = Math.min(blockSize, imageWidth-j);
				for (int k=0; k<blockRemain; k++)
					pixIter[ind++] = numIter;

				u += du;
			}
			// Duplicate current line to fill blocks. Only effective if blockSize>1.
			int blockRemain = Math.min(blockSize, imageHeight-i)-1;
			for (int k=0; k<blockRemain; k++) {
				System.arraycopy(pixIter, indPrev, pixIter, ind, imageWidth);
				ind += imageWidth;
			}
			v += dv;
		}

		// PsDebug.message("Seconds per frame = "+PsDebug.getTimeUsed());
	}


	public void computeNiuton(int [] pixIter, int imageWidth, int imageHeight, int blockSize,
											double xMin, double yMin, double xMax, double yMax, int maxIter, PuComplex c2) {
		PuComplex z	= new PuComplex();
		PuComplex c	= new PuComplex();
		double du	= blockSize*(xMax-xMin)/(imageWidth-1.);
		double dv	= blockSize*(yMax-yMin)/(imageHeight-1.);

		// PsDebug.initTime();

		int ind		= 0;
		double v		= yMin;
        
		for (int i=0; i<imageHeight; i+=blockSize) {
			int indPrev = ind;
			double u		= xMin;
			for (int j=0; j<imageWidth; j+=blockSize) {
				
				z.set(u, v);

				int numIter	= 0;
				for (int k=0; k<maxIter; k++) {

                    
                    if (mode == 0)
                    {
                       
                        c = PuComplex.pow(z, 2).add(new PuComplex(c2).neg());
                        z = new PuComplex(c).div(new PuComplex(z).mult(2));
                        //c = PuComplex.pow(z, 3).add(-1);
                       //z = z.add(new PuComplex(c).div(PuComplex.pow(z, 2).mult(3)).neg());
                    } else {
                      PuComplex zPow2 = PuComplex.pow(z, 2);
                      PuComplex zPow4 = PuComplex.pow(zPow2, 2);
                      PuComplex zPow5 = new PuComplex(zPow4).mult(z);
                      PuComplex zPow6 = new PuComplex(zPow4).mult(zPow2);
                      c = new PuComplex(zPow2).mult(zPow5).add(new PuComplex(zPow5).neg()).add(c2);
                      PuComplex differ = zPow6.mult(7).add(new PuComplex(zPow4).mult(5).neg());
                      z = z.add(new PuComplex(c).div(differ).neg());
                    }
					// If sequence diverges then note the used number of iterations and break.
					if (c.sqrAbs() < 0.04) {
						numIter		= k+1;
						break;
					}
				}

				int blockRemain = Math.min(blockSize, imageWidth-j);
				for (int k=0; k<blockRemain; k++)
					pixIter[ind++] = numIter;

				u += du;
			}

			int blockRemain = Math.min(blockSize, imageHeight-i)-1;
			for (int k=0; k<blockRemain; k++) {
				System.arraycopy(pixIter, indPrev, pixIter, ind, imageWidth);
				ind += imageWidth;
			}
			v += dv;
		}

		// PsDebug.message("Seconds per frame = "+PsDebug.getTimeUsed());
	}

	/**
	 * Compute color array from an array of scalar integer values.
	 * Transfer function depends on choosen color type.
	 */
	public void computeColors(int [] pixArr, int [] valArr, int len, int maxVal, int colType) {
		m_colMap.setSize(maxVal);
		int hueOffset	= m_hueOffset.getValue();
		for (int k=0; k<maxVal; k++) {
			int col = (0 << 24);
			if (k == 0) {
				col = (255 << 24)|(55 << 16)|(55 << 8)|(55);
			} else {
				int hue = 0;
				switch (colType) {
				case COLOR_REDBLACK:
					col = (255 << 24) |
							((50+205*k/maxVal)%255 << 16) |
							(0 << 8) |
							0;
					break;
				case COLOR_HUEOFFSET:
					hue += hueOffset;
				case COLOR_HUE:
					hue += (int)(205*k/maxVal);
					// Looping would correctly cycle thru colors but looks pretty without looping too.
					// if (hue > 255)
					// 	hue -= 255;
					col = PdColor.hsv2rgbAsInt(hue, 255, 255);
					break;
				case COLOR_BLACK:
					col = (0 << 24);
					break;
				}
			}
			m_colMap.m_data[k] = col;
		}
		for (int i=0; i<len; i++) {
			int numIter	= valArr[i];
			pixArr[i]	= m_colMap.m_data[numIter];
		}
	}
}
