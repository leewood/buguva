package Chaos1;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;


import jv.number.PdVector_IP;
import jv.object.PsDebug;
import jv.object.PsPanel;
import jv.object.PsUpdateIf;
import jv.objectGui.PsMultiLineLabel;
import jv.project.PjProject_IP;
import jv.project.PvDisplayIf;
import jv.vecmath.PdVector;

/**
 * Inspector of Julia set explorer.
 * 
 * @author		Konrad Polthier
 * @version		01.10.05, 2.10 revised (kp) User interface improved.<br>
 *					10.08.05, 2.00 revised (kp) Functional extensions and efficiency optimization.<br>
 *					26.09.99, 1.00 created (kp) 
 */
public class PjFractalImage_IP extends PjProject_IP implements ActionListener, java.awt.event.ItemListener {
	protected	PjFractalImage			m_pjFractal;
	protected	PsPanel					m_pSlider;
	protected	PsPanel					m_pMandelbrot;
	/** Copy of Julia set constant. */
	protected	PdVector					m_juliaConst;
	/** Inspector of Julia set constant. */
	protected	PdVector_IP				m_pJuliaConst;
	protected	Button					m_bReset;
    protected   java.awt.Checkbox                m_cBPaintNiuton;
    protected   java.awt.Checkbox                m_cBPaintMode;

	public PjFractalImage_IP() {
		super();
		m_juliaConst			= new PdVector(2);
		m_pJuliaConst			= new PdVector_IP();
		m_pJuliaConst.setTitle("Julijos parametras");
		m_pJuliaConst.setParent(this);
		m_pJuliaConst.setVector(m_juliaConst);

		if (getClass() == PjFractalImage_IP.class)
			init();
	}
	public void init() {
		super.init();
		//addTitle("");

//		PsPanel pNotice = new PsPanel();
		//pNotice.setInsetSize(5);
		//pNotice.setBorderType(PsPanel.BORDER_GROOVE);
		//pNotice.add(new PsMultiLineLabel(getNotice()));
		//add(pNotice);
		
		// Container of Mandelbrot display, if running as project from JavaView application.
		m_pMandelbrot = new PsPanel();
		{
			m_pMandelbrot.setLayout(new BorderLayout());
		}
		add(m_pMandelbrot);

		add(m_pJuliaConst);
		
		m_pSlider = new PsPanel();
		add(m_pSlider);
		
		// buttons at bottom
		Panel m_pBottomButtons = new Panel();
		m_pBottomButtons.setLayout(new FlowLayout(FlowLayout.CENTER));
		add(m_pBottomButtons);
		m_bReset = new Button("Iš naujo");
        m_cBPaintNiuton = new java.awt.Checkbox("Piešti niutono transformaciją");
        m_cBPaintNiuton.setState(false);
        m_cBPaintMode = new java.awt.Checkbox("Išplėstinis rėžimas");
        m_cBPaintMode.setState(false);
        m_cBPaintMode.addItemListener(this);
		m_bReset.addActionListener(this);
        m_cBPaintNiuton.addItemListener(this);
        m_pBottomButtons.add(m_cBPaintNiuton);
        m_pBottomButtons.add(m_cBPaintMode);
		m_pBottomButtons.add(m_bReset);
	}
	private	String	getNotice() {
		String notice = "";
		return notice;
	}
	/**
	 * Set parent of panel which supplies the data inspected by the panel.
	 */
	public void setParent(PsUpdateIf parent) {
		super.setParent(parent);
		m_pjFractal = (PjFractalImage)parent;
		setTitle("");
		m_pSlider.add(m_pjFractal.m_maxIter.getInfoPanel());
		m_pSlider.add(m_pjFractal.m_blockSize.getInfoPanel());
		m_pSlider.add(m_pjFractal.m_hueOffset.getInfoPanel());
		PvDisplayIf dispMandelbrot	= m_pjFractal.getDispMandelbrot();
		if (!m_pjFractal.hasDisplay(dispMandelbrot)) {
			m_pMandelbrot.setPreferredSize(320, 256);
			m_pMandelbrot.add((Component)dispMandelbrot);
			validate();
		}
	}
	/**
	 * Update the panel whenever the parent has changed somewhere else.
	 * Method is invoked from the parent or its superclasses.
	 */
	public boolean update(Object event) {
		if (m_pjFractal == event) {
			/*
			Component [] comp = m_pMandelbrot.getComponents();
			if (m_pjFractal.m_type == PjFractalImage.JULIA) {
				if (comp==null || comp.length==0) {
					m_pMandelbrot.add((Component)m_pjFractal.m_dispMandelbrot, BorderLayout.CENTER);
					m_pMandelbrot.validate();
				}
			} else {
				if (comp!=null && comp.length>0) {
					m_pMandelbrot.remove((Component)m_pjFractal.m_dispMandelbrot);
					m_pMandelbrot.validate();
				}
			}
			*/
			m_juliaConst.set(m_pjFractal.m_const.re, m_pjFractal.m_const.im);
			m_pJuliaConst.update(m_juliaConst);
			return true;
		} else if (m_pJuliaConst == event) {
			m_pjFractal.m_const.set(m_juliaConst.getEntry(0), m_juliaConst.getEntry(1));
			m_pjFractal.update(m_pjFractal.m_const);
			return true;
		}
		return super.update(event);
	}
	/**
	 * Handle action events invoked from buttons, menu items, text fields.
	 */

    public void itemStateChanged(java.awt.event.ItemEvent event)
    {
		if (m_pjFractal==null)
			return;
        Object source = event.getSource();
        if (source == this.m_cBPaintNiuton)
        {
            m_pjFractal.setPaintNiuton(m_cBPaintNiuton.getState());
			m_pjFractal.init();
			m_pjFractal.start();
            m_pjFractal.update(m_pjFractal);
        } else if (source == this.m_cBPaintMode)
        {
            m_pjFractal.setMode(this.m_cBPaintMode.getState());
			m_pjFractal.init();
			m_pjFractal.start();
            m_pjFractal.update(m_pjFractal);
        }
    }

	public void actionPerformed(ActionEvent event) {
		if (m_pjFractal==null)
			return;
		Object source = event.getSource();
		if (source == m_bReset) {
			m_pjFractal.init();
			m_pjFractal.start();
			m_pjFractal.update(m_pjFractal);
		}
	}
}

