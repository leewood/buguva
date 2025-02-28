package vgp.tutor.fractal;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Image;
import java.awt.Point;
import java.awt.event.ComponentEvent;
import java.awt.event.ComponentListener;
import java.awt.image.MemoryImageSource;

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
import jv.vecmath.PiVector;

/**
 * Demo project for working with pixel images. Project displays a Julia set
 * and a Mandelbrot set where the Julia set is determined by picking a module
 * value in the Mandelbrot image. Picking in a Julia set will display several
 * iterations of the picked point.
 * <p>
 * Zoom into images by marking a rectangle in display using mark-mode.
 * 
 * @author		Konrad Polthier
 * @version		01.10.05, 2.10 revised (kp) User interface improved.<br>
 *					10.08.05, 2.00 revised (kp) Functional extensions and efficiency optimization.<br>
 *					26.09.99, 1.00 created (kp)
 */
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

	public PjFractalImage() {
		super("Julia Set Explorer");
		
		m_pix						= new PiVector();
		m_pixIter				= new PiVector();
		m_pixStore				= new PiVector();
		
		m_pixMandelbrot		= new PiVector();
		m_pixIterMandelbrot	= new PiVector();
		m_pixStoreMandelbrot	= new PiVector();
		
		m_pixTrace				= new PiVector();
		m_colMap					= new PiVector();
		
		m_bounds					= new PdVector(-2.0, -1.5, 2.0, 1.5);
		m_boundsMandelbrot	= new PdVector(-2.2, -1., 0.8, 1.);
		
		m_maxIter				= new PuInteger("Number of Iterations", this);
		m_blockSize				= new PuInteger("Block Size", this);
		m_hueOffset				= new PuInteger("Hue Offset", this);
		
		if (getClass() == PjFractalImage.class)
			init();
	}
	public void init() {
		super.init();
		m_image					= null;
		m_imageMandelbrot		= null;
		
		m_pix.setSize(0);
		m_pixIter.setSize(0);
		m_pixStore.setSize(0);

		m_pixMandelbrot.setSize(0);
		m_pixIterMandelbrot.setSize(0);
		m_pixStoreMandelbrot.setSize(0);
		
		m_imageHeight			= 0;
		m_imageWidth			= 0;
		m_imageHeightMandelbrot	= 0;
		m_imageWidthMandelbrot	= 0;
		
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
		// Determine rectangular section in the complex plane
		// where the Julia and Mandelbrot sets are computed.
		m_bounds.set(-2.0, -1.5, 2.0, 1.5);
		m_boundsMandelbrot.set(-2.2, -1., 0.8, 1.);
		
		// Adjust sizes of images to dimension of display canvas
		if (resizeImage(m_dispJulia)) {
			computeImageJulia(m_dispJulia,
									m_bounds.m_data[0], m_bounds.m_data[1], m_bounds.m_data[2], m_bounds.m_data[3]);
			m_dispJulia.update(null);
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
			m_dispJulia.showBackgroundImage(true);
		}
		if (m_dispMandelbrot != null) {
			m_dispMandelbrot.selectCamera(PvCameraIf.CAMERA_ORTHO_XY);
			m_dispMandelbrot.setBackgroundImageFit(PvDisplayIf.IMAGE_RESIZE);
			m_dispMandelbrot.setMajorMode(PvDisplayIf.MODE_INITIAL_PICK);
			m_dispMandelbrot.showBackgroundImage(true);
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
	}
	/**
	 * Get display of Julia set. The shown Julia set is determined
	 * by a complex value c to be picked in the Mandelbrot image.
	 * Picking inside the Julia set will show the iterated values
	 * of the picked point z under the map [z -> z^2+c].
	 */
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
					z.sqr().add(c);
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
	public void computeMandelbrot(int [] pixIter, int imageWidth, int imageHeight, int blockSize,
											double xMin, double yMin, double xMax, double yMax, int maxIter) {
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
				c.set(u, v);
				z.set(u, v);
				
				// This is Mandelbrot iteration f(z)=z*z+c with c=z0.
				// For each z check whether iteration diverges to infinity
				int numIter	= 0;
				for (int k=0; k<maxIter; k++) {
					z.sqr().add(c);
					// If sequence diverges then note the used number of iterations and break.
					if (z.sqrAbs() > 4.) {
						numIter		= k+1;
						break;
					}
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
