package lt.vu.mif.gt.mapviewer;

import java.awt.Color;
import java.awt.Component;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Point;
import java.awt.Rectangle;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Random;

import javax.swing.DefaultListModel;
import javax.swing.JColorChooser;
import javax.swing.JFileChooser;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTable;
import javax.swing.SwingUtilities;
import javax.swing.SwingWorker;
import javax.swing.filechooser.FileNameExtensionFilter;
import javax.swing.table.DefaultTableModel;

import lt.vu.mif.gt.mapviewer.utils.ProgressBarDialog;

import org.geotools.data.FeatureSource;
import org.geotools.data.memory.MemoryDataStore;
import org.geotools.data.shapefile.ShapefileDataStore;
import org.geotools.factory.CommonFactoryFinder;
import org.geotools.factory.GeoTools;
import org.geotools.feature.FeatureCollection;
import org.geotools.feature.simple.SimpleFeatureBuilder;
import org.geotools.feature.simple.SimpleFeatureTypeBuilder;
import org.geotools.filter.text.cql2.CQL;
import org.geotools.filter.text.cql2.CQLException;
import org.geotools.geometry.jts.FactoryFinder;
import org.geotools.geometry.jts.JTSFactoryFinder;
import org.geotools.geometry.jts.ReferencedEnvelope;
import org.geotools.map.DefaultMapContext;
import org.geotools.map.DefaultMapLayer;
import org.geotools.map.MapContext;
import org.geotools.map.MapLayer;
import org.geotools.referencing.CRS;
import org.geotools.renderer.shape.ShapefileRenderer;
import org.geotools.styling.Graphic;
import org.geotools.styling.LineSymbolizer;
import org.geotools.styling.Mark;
import org.geotools.styling.PointSymbolizer;
import org.geotools.styling.PolygonSymbolizer;
import org.geotools.styling.Style;
import org.geotools.styling.StyleBuilder;
import org.opengis.feature.Feature;
import org.opengis.feature.Property;
import org.opengis.feature.simple.SimpleFeature;
import org.opengis.feature.simple.SimpleFeatureType;
import org.opengis.feature.type.AttributeDescriptor;
import org.opengis.feature.type.FeatureType;
import org.opengis.filter.FilterFactory2;
import org.opengis.geometry.BoundingBox;
import org.opengis.geometry.coordinate.LineString;
import org.opengis.geometry.coordinate.Polygon;
import org.opengis.referencing.FactoryException;
import org.opengis.referencing.NoSuchAuthorityCodeException;
import org.opengis.referencing.crs.CoordinateReferenceSystem;

import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Envelope;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryCollection;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.vividsolutions.jts.geom.MultiLineString;
import com.vividsolutions.jts.geom.MultiPolygon;

public class MapPanel extends JPanel implements MouseListener
{

    private static final long serialVersionUID = -6716531331420451997L;
    private MapContext mapContext = null;
    private ShapefileRenderer renderer = null;
    BufferedImage image = null;

    public void setListView(JList jl)
    {
        output = jl;
    }

    ProgressBarDialog dialog = null;

    MainWindow wind = null;

    public MapPanel(ProgressBarDialog dialog, MainWindow main)
    {
        this.dialog = dialog;
        wind = main;
        image = null;
        // addMapLayer("data\\apskrity.shp", "No name");

    }

    Rectangle2D vizArea = null;

    public java.awt.geom.Point2D FromFramePoint(Point p)
    {
        java.awt.geom.Point2D result = new java.awt.geom.Point2D.Double();
        double x = env.getMinX()
                + (p.x * ((env.getMaxX() - env.getMinX()) / MaxWidth));
        double y = env.getMinY()
                + ((MaxHeight - p.y) * ((env.getMaxY() - env.getMinY()) / MaxHeight));
        result.setLocation(x, y);
        return result;
    }

    ReferencedEnvelope FromScreenRectangle(Rectangle rect)
    {
        Point p1 = new Point((int) rect.getMaxX(), (int) rect.getMaxY());
        Point p2 = new Point((int) rect.getMinX(), (int) rect.getMinY());
        Point2D p11 = this.FromFramePoint(p1);
        Point2D p22 = this.FromFramePoint(p2);
        ReferencedEnvelope newEnv = new ReferencedEnvelope(p22.getX(), p11
                .getX(), p22.getY(), p11.getY(), mapContext
                .getCoordinateReferenceSystem());
        return newEnv;
    }

    public void mouseExited(MouseEvent e)
    {
    }

    public void zoomSel()
    {
        if (highlightLayer != null)
        {
            try
            {
                env = highlightLayer.getBounds();
                if ((env.getWidth() == 0) || (env.getHeight() == 0))
                {
                    env = new ReferencedEnvelope(env.getMinX() - 1, env
                            .getMaxX() + 1, env.getMinY() - 1,
                            env.getMaxY() + 1, mapContext
                                    .getCoordinateReferenceSystem());
                }

            } catch (Exception e)
            {

                try
                {
                    Iterator<SimpleFeature> iter = highlightLayer
                            .getFeatureSource().getFeatures().iterator();
                    SimpleFeature feature = iter.next();
                    env = (ReferencedEnvelope) feature.getBounds();
                } catch (Exception e1)
                {
                    // TODO Auto-generated catch block

                }

            }
        }
        repaint();
    }

    public boolean SpecificFunction = false;

    public void mouseReleased(MouseEvent e)
    {
        if ((e.isControlDown()) || (this.getTool() == MapPanel.ZOOMTOEXTENT))
        {
            updateSize(e);
            Rectangle tempRect = currentRect;
            currentRect = null;
            zoomRectangle(FromScreenRectangle(tempRect));
        } else if (e.isShiftDown() || (this.getTool() == MapPanel.ZOOMIN)
                || (getTool() == ZOOMOUT))
        {
            if ((e.getButton() == MouseEvent.BUTTON1)
                    || (this.getTool() == MapPanel.ZOOMIN))
            {
                setZoomIn(FromFramePoint(e.getPoint()));
            } else
            {
                setZoomOut(FromFramePoint(e.getPoint()));
            }

        } else if (e.isAltDown() || (this.getTool() == MapPanel.PAN))
        {
        } else
        {
            updateSize(e);
            Rectangle tempRect = currentRect;
            currentRect = null;
            Point p1 = new Point();
            Point p2 = new Point();
            p1.setLocation(tempRect.getMinX(), tempRect.getMinY());
            p2.setLocation(tempRect.getMaxX(), tempRect.getMaxY());
            boolean oneMode = ((tempRect.getWidth() < 2) || (tempRect
                    .getHeight() < 2))
                    && !(getTool() == MapPanel.GETAREA);
            if (oneMode)
            {
                Point p = e.getPoint();
                p.setLocation(p.getX() - this.getLocation().getX(), p.getY()
                        - this.getLocation().getY());
                // p1.setLocation(p);
                // p2.setLocation(p);
                p1.setLocation((p2.getX() + p1.getX()) / 2, (p2.getY() + p1
                        .getY()) / 2);
                p1.setLocation(p1.getX() - 1, p1.getY() - 1);
                p2.setLocation(p2.getX() + 1, p2.getY() + 1);
            }
            this.getFeature(FromFramePoint(p1), FromFramePoint(p2), oneMode);
            if (SpecificFunction)
            {
                this.PerformSpecialFunction();
            }
        }
    }

    public void mouseEntered(MouseEvent e)
    {
    }

    public void mousePressed(MouseEvent e)
    {

        if (e.isShiftDown() || (this.getTool() == MapPanel.ZOOMIN)
                || (getTool() == ZOOMOUT))
        {
        } else if (e.isAltDown() || (getTool() == PAN))
        {
            oldX = e.getX();
            oldY = e.getY();
            currentRect = null;
            updatePanning(e);
        } else if (e.isControlDown() || (getTool() == MapPanel.ZOOMTOEXTENT))
        {
            int x = e.getX();
            int y = e.getY();
            currentRect = new Rectangle(x, y, 0, 0);
            updateDrawableRect(getWidth(), getHeight());
        } else
        {
            if (e.getButton() == MouseEvent.BUTTON1)
            {
                int x = e.getX();
                int y = e.getY();
                currentRect = new Rectangle(x, y, 0, 0);
                updateDrawableRect(getWidth(), getHeight());

                // setZoomIn(FromFramePoint(e.getPoint()));
            } else
            {
                // setZoomOut(FromFramePoint(e.getPoint()));
            }
        }

    }

    public void mouseMoved(MouseEvent e)
    {
        if ((e.isControlDown() || (this.getTool() == MapPanel.ZOOMTOEXTENT)))
        {
            updateSize(e);
        } else if (e.isAltDown() || (this.getTool() == MapPanel.PAN))
        {
            updatePanning(e);
        }
        if (e.isShiftDown())
        {

        } else
        {
            updateSize(e);
        }

    }

    public void selectAll()
    {

        Point2D pnt = new java.awt.geom.Point2D.Double();
        ReferencedEnvelope e;
        try
        {
            e = mapContext.getLayerBounds();
            pnt.setLocation(e.getMinX(), e.getMinY());
            Point2D pnt2 = new java.awt.geom.Point2D.Double();
            pnt2.setLocation(e.getMaxX(), e.getMaxY());
            this.getFeature(pnt, pnt2, false);

        } catch (IOException e1)
        {
            // TODO Auto-generated catch block
            e1.printStackTrace();
        }

    }

    public void mouseClicked(MouseEvent e)
    {
        /*
         * if (e.isShiftDown() || (this.getTool() == MapPanel.ZOOMIN) ||
         * (getTool() == ZOOMOUT)) { } else if (e.isAltDown() || (getTool() ==
         * PAN)) { oldX = e.getX(); oldY = e.getY(); //System.out.println("Cia:"
         * + oldX + " " + oldY); updatePanning(e); } else if
         * (!e.isControlDown()) { if (e.getButton() == MouseEvent.BUTTON1) {
         * Point p1 = new Point(); Point p2 = new Point();
         * p1.setLocation((int)(e.getPoint().getX() - 1),
         * (int)(e.getPoint().getY() - 1));
         * p2.setLocation((int)(e.getPoint().getX() + 1),
         * (int)(e.getPoint().getY() + 1)); this.getFeature(FromFramePoint(p1),
         * FromFramePoint(p2), true);
         * 
         * //setZoomIn(FromFramePoint(e.getPoint())); } else {
         * //setZoomOut(FromFramePoint(e.getPoint())); } } else {
         * //area.processMouseEv(e); System.out.println("StartDrag"); int x =
         * e.getX(); int y = e.getY(); currentRect = new Rectangle(x, y, 0, 0);
         * updateDrawableRect(getWidth(), getHeight());
         * 
         * }
         */
    }

    Rectangle currentRect = null;
    Rectangle rectToDraw = null;
    Rectangle previousRectDrawn = new Rectangle();
    JList output = null;

    public ArrayList<String[]> getAttributes(SimpleFeature feature)
    {
        ArrayList<String[]> result = new ArrayList<String[]>();
        java.util.List<AttributeDescriptor> desc = feature.getFeatureType()
                .getAttributeDescriptors();
        for (int i = 0; i < feature.getAttributeCount(); i++)
        {
            Object attr = feature.getAttribute(i);

            String[] line = new String[2];
            line[0] = desc.get(i).getName().toString();
            line[1] = attr.toString();
            result.add(line);
        }
        Iterator<Property> iter = feature.getProperties().iterator();
        while (iter.hasNext())
        {
            Property prop = iter.next();
            String[] line = new String[2];
            line[0] = prop.getName().toString();
            line[1] = prop.getValue().toString();
        }
        return result;
    }

    JTable table = null;

    public void setTable(JTable tb)
    {
        table = tb;
    }

    public void outputToListView(JList output, SimpleFeature feature)
    {
        if (output != null)
        {
            ArrayList<String[]> list = getAttributes(feature);
            DefaultListModel model = (DefaultListModel) output.getModel();
            model.removeAllElements();
            for (int i = 0; i < list.size(); i++)
            {
                String[] item = list.get(i);
                model.addElement(item[0] + " = " + item[1]);
            }
            output.repaint();
        }
    }

    public void outputToTable(SimpleFeature[] features)
    {
        table.setModel(new DefaultTableModel(new Object[0][0],
                new String[] { "Value" }));
        if (features.length > 0)
        {
            int attrCount = features[0].getAttributeCount();
            Object[][] values = new Object[features.length][attrCount];
            String[] names = new String[attrCount];
            for (int i = 0; i < attrCount; i++)
            {
                names[i] = features[0].getFeatureType()
                        .getAttributeDescriptors().get(i).getName().toString();
            }
            for (int i = 0; i < features.length; i++)
            {
                for (int j = 0; j < attrCount; j++)
                {
                    values[i][j] = features[i].getAttribute(j).toString();
                }
            }
            table.setModel(new DefaultTableModel(values, names));
        }

    }

    LayersPanel layersPanel = null;

    public void setLayersPanel(LayersPanel lPanel)
    {
        layersPanel = lPanel;
    }

    private void updateDrawableRect(int compWidth, int compHeight)
    {

        if (currentRect == null)
        {
            currentRect = new Rectangle(0, 0, compWidth, compHeight);
        }
        int x = currentRect.x;
        int y = currentRect.y;
        int width = currentRect.width;
        int height = currentRect.height;
        if (width < 0)
        {
            width = 0 - width;
            x = x - width + 1;
            if (x < 0)
            {
                width += x;
                x = 0;
            }
        }
        if (height < 0)
        {
            height = 0 - height;
            y = y - height + 1;
            if (y < 0)
            {
                height += y;
                y = 0;
            }
        }
        if ((x + width) > compWidth)
        {
            width = compWidth - x;
        }
        if ((y + height) > compHeight)
        {
            height = compHeight - y;
        }
        if (rectToDraw != null)
        {
            previousRectDrawn.setBounds(rectToDraw.x, rectToDraw.y,
                    rectToDraw.width, rectToDraw.height);
            rectToDraw.setBounds(x, y, width, height);
        } else
        {
            rectToDraw = new Rectangle(x, y, width, height);
        }
    }

    void updateSize(MouseEvent e)
    {
        int x = e.getX();
        int y = e.getY();
        if (currentRect != null)
        {
            currentRect.setSize(x - currentRect.x, y - currentRect.y);
        } else
        {
            currentRect = new Rectangle(x, y, 0, 0);
        }
        updateDrawableRect(getWidth(), getHeight());
        Rectangle totalRepaint = rectToDraw.union(previousRectDrawn);
        System.out.println("Updated");
        repaint(totalRepaint.x, totalRepaint.y, totalRepaint.width,
                totalRepaint.height);
    }

    protected void setZoomIn(java.awt.geom.Point2D pnt)
    {

        double width = env.getWidth() / ZoomFactor;
        double height = env.getHeight() / ZoomFactor;
        double x = pnt.getX() - (0.5 * width);
        double y = pnt.getY() - (0.5 * height);
        double x2 = pnt.getX() + (0.5 * width);
        double y2 = pnt.getY() + (0.5 * height);
        env = new ReferencedEnvelope(x, x2, y, y2, mapContext
                .getCoordinateReferenceSystem());
        this.repaint();
    }

    boolean continueSpecialFunction = false;

    @SuppressWarnings("unchecked")
    protected void getFeature(java.awt.geom.Point2D pnt,
            java.awt.geom.Point2D pnt2, boolean oneMode)
    {
        continueSpecialFunction = false;
        System.out.println("Starting");
        MapLayer queryLayer = mapContext.getLayer(getActiveLayer());
        if (queryLayer == highlightLayer)
        {
            queryLayer = mapContext.getLayer(mapContext.getLayerCount() - 2);
        }
        String ss = queryLayer.getFeatureSource().getSchema()
                .getGeometryDescriptor().getLocalName();
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        ReferencedEnvelope bounds = new ReferencedEnvelope(pnt.getX(), pnt2
                .getX(), pnt.getY(), pnt2.getY(), mapContext
                .getCoordinateReferenceSystem());
        if (oneMode)
        {
            bounds = new ReferencedEnvelope(pnt.getX(), pnt2.getX(),
                    pnt.getY(), pnt2.getY(), mapContext
                            .getCoordinateReferenceSystem());
        }
        org.opengis.filter.Filter boundsCheck = ff.bbox(
                ff.property("the_geom"), bounds);
        org.opengis.filter.Filter polygonCheck = ff.not(ff.disjoint(ff
                .property(ss), ff.literal(bounds)));
        org.opengis.filter.Filter filter = ff.and(boundsCheck, polygonCheck);
        try
        {
            FeatureSource fs = queryLayer.getFeatureSource();
            FeatureCollection fr = fs.getFeatures(filter);
            if (fr != null)
            {

                if ((fr.size() > 1) && (oneMode))
                {
                    System.out.println("More than one");
                    Envelope vividEnv = fr.getBounds();
                    Rectangle2D rect = new Rectangle2D.Double(vividEnv
                            .getMinX(), vividEnv.getMinY(),
                            vividEnv.getWidth(), vividEnv.getHeight());
                    env = new ReferencedEnvelope(rect.getMinX(),
                            rect.getMaxX(), rect.getMinY(), rect.getMaxY(),
                            mapContext.getCoordinateReferenceSystem());
                    this.repaint();
                    JOptionPane.showMessageDialog(null,
                            "More than one feature in this area. Zooming",
                            "Error", JOptionPane.WARNING_MESSAGE);
                } else if (fr.size() == 0)
                {
                    if (oneMode)
                    {
                        JOptionPane.showMessageDialog(null,
                                "No features in this area", "Error",
                                JOptionPane.WARNING_MESSAGE);
                    }
                    System.out.println("No features");
                } else if ((fr.size() > 1) && (!oneMode))
                {
                    Iterator<SimpleFeature> reader = (Iterator<SimpleFeature>) fr
                            .iterator();
                    SimpleFeature f = null;
                    ArrayList<SimpleFeature> fArr = new ArrayList<SimpleFeature>();
                    while (reader.hasNext())
                    {
                        f = reader.next();
                        fArr.add(f);
                    }
                    SimpleFeature[] fArr2 = new SimpleFeature[fArr.size()];
                    for (int i = 0; i < fArr.size(); i++)
                    {
                        fArr2[i] = fArr.get(i);
                    }
                    highlightFeatures(fArr2);
                } else
                {
                    System.out.println("One");
                    Iterator<SimpleFeature> reader = (Iterator<SimpleFeature>) fr
                            .iterator();
                    SimpleFeature f = null;
                    SimpleFeature fArr[] = new SimpleFeature[1];
                    while (reader.hasNext())
                    {
                        f = reader.next();
                    }
                    fArr[0] = f;
                    highlightFeatures(fArr);
                    continueSpecialFunction = true;
                    // this.outputToListView(output, f);
                }
            } else
            {
                System.out.println("Nulas");
            }
        } catch (Exception e1)
        {
            e1.printStackTrace();
        }
    }

    public void selectByAttribute(String attributeName, String attributeValue,
            int id)
    {
        SimpleFeature feature = null;
        if (highlightLayer != null)
        {
            try
            {
                Iterator<SimpleFeature> iter = highlightLayer
                        .getFeatureSource().getFeatures().iterator();
                for (int i = 0; i < id; i++)
                {
                    iter.next();
                }
                feature = iter.next();
            } catch (IOException e)
            {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }

        }
        if (!search(attributeName + " = " + attributeValue, false))
        {
            search(attributeName + " = '" + attributeValue + "'", false);
        }
        if (!successSelectByAttribute)
        {
            if (feature != null)
            {
                highlightFeatures(new SimpleFeature[] { feature });
                this.repaint();
            }
        }

    }

    boolean successSelectByAttribute = true;

    public boolean search(String cqlSentence)
    {
        return search(cqlSentence, true);
    }

    int lastActiveBeforeFunction = 0;
    int lastToolBeforeFunction = 0;
    double lastGX = 0;
    double lastGY = 0;
    double lastGM = 0;
    double lastGN = 0;
    double lastGZ = 0;
    double lastGK = 0;
    double lastGH = 0;
    String lastC1 = "";
    String lastC2 = "";
    String lastC3 = "";

    public void InitSpecialFunction(double x, double y, double m, double n,
            double z, double k, double h, String c1, String c2, String c3)
    {
        this.SpecificFunction = true;
        JOptionPane.showMessageDialog(null, "Choose district", "Info",
                JOptionPane.INFORMATION_MESSAGE);
        lastActiveBeforeFunction = this.getActiveLayer();
        this.setActiveLayer(0);
        lastToolBeforeFunction = this.getTool();
        this.setTool(GETFEATURE);
        lastGX = x;
        lastGY = y;
        lastGM = m;
        lastGN = n;
        lastGZ = z;
        lastGK = k;
        lastGH = h;
        lastC1 = c1;
        lastC2 = c2;
        lastC3 = c3;
    }

    void updateStatus(final int i)
    {
        Runnable doSetProgressBarValue = new Runnable()
        {
            public void run()
            {
                dialog.setDValue(i);
                dialog.repaint();
            }
        };
        SwingUtilities.invokeLater(doSetProgressBarValue);
    }

    void updateTitle(final String title)
    {
        Runnable doSetProgressBarValue = new Runnable()
        {
            public void run()
            {
                dialog.setDText(title);
                dialog.repaint();
            }
        };
        SwingUtilities.invokeLater(doSetProgressBarValue);
    }

    private Component me = this;

    void initStatus(final String text, final String title, final int max)
    {
        Runnable doSetProgressBarValue = new Runnable()
        {
            public void run()
            {

                dialog = new ProgressBarDialog();
                dialog.setLocationRelativeTo(me);
                dialog.reset();
                dialog.setDText(text);
                dialog.setDTitle(title);
                dialog.setVisible(true);
                dialog.setDMaxValue(max);
                dialog.repaint();
            }
        };
        SwingUtilities.invokeLater(doSetProgressBarValue);

    }

    void JavaWork(final SimpleFeature SelectedDistrict, final double x,
            final double y, final double m, final double n, final double z,
            final double k, final double h, final String c1, final String c2,
            final String c3)
    {
        SwingWorker worker = new SwingWorker()
        {
            @Override
            protected Object doInBackground() throws Exception
            {
                SpecialFunctionWorker(SelectedDistrict, x, y, m, n, z, k, h,
                        c1, c2, c3);
                return "";
            }
        };
        worker.execute();
    }

    public void PerformSpecialFunction()
    {
        if (this.continueSpecialFunction)
        {
            try
            {
                // SelectedDistrict =
                Iterator<SimpleFeature> iter = highlightLayer
                        .getFeatureSource().getFeatures().iterator();
                if (iter.hasNext())
                {
                    SelectedDistrict = iter.next();
                    env = (ReferencedEnvelope) SelectedDistrict.getBounds();
                }

            } catch (IOException e)
            {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
            this.setActiveLayer(lastActiveBeforeFunction);
            this.setTool(lastToolBeforeFunction);
            this.repaint();
            JavaWork(SelectedDistrict, this.lastGX, this.lastGY, this.lastGM,
                    this.lastGN, this.lastGZ, this.lastGK, this.lastGH,
                    this.lastC1, this.lastC2, this.lastC3);
            SpecificFunction = false;
        }
    }

    private SimpleFeature SelectedDistrict = null;

    private SimpleFeature NearestRiver(SimpleFeature city, MapLayer rivers)
    {
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        org.opengis.filter.Filter filter = ff
                .dwithin(ff.property("the_geom"), ff.literal(((Geometry) city
                        .getDefaultGeometry()).buffer(1000)), 0, "");
        try
        {
            FeatureCollection<? extends FeatureType, ? extends Feature> features = rivers
                    .getFeatureSource().getFeatures(filter);
            return FromCollection(features)[0];
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        return null;
    }

    private SimpleFeature NearestCity(SimpleFeature area, MapLayer cities,
            double n)
    {
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        org.opengis.filter.Filter filter = ff.dwithin(ff.property("the_geom"),
                ff.literal(((Geometry) area.getDefaultGeometry()).buffer(n)),
                0, "");
        try
        {
            FeatureCollection<? extends FeatureType, ? extends Feature> features = cities
                    .getFeatureSource().getFeatures(filter);
            return FromCollection(features)[0];
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        return null;

    }

    private SimpleFeature[] RiverParts(SimpleFeature river, MapLayer surface)
    {
        ArrayList<SimpleFeature> list = new ArrayList<SimpleFeature>();
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        Geometry riverGeom = (Geometry) river.getDefaultGeometry();
        int id = 0;// (Integer) river.getProperty("UPES_ID").getValue();
        System.out.println(riverGeom.getClass().getName());
        org.opengis.filter.Filter filter = ff.dwithin(ff.literal(river
                .getDefaultGeometry()), ff.property("the_geom"), 0, "");
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("RiverPart");
        builder.setNamespaceURI("http://localhost/");
        try
        {
            builder.setCRS(CRS.decode("EPSG:2600"));
        } catch (NoSuchAuthorityCodeException e)
        {
            e.printStackTrace();
        } catch (FactoryException e)
        {
            e.printStackTrace();
        }
        builder.add("the_geom", riverGeom.getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        builder.add("ILGIS", double.class);
        builder.add("AUKSTIS", double.class);
        builder.add("UPEID", int.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();

        try
        {
            FeatureCollection col = surface.getFeatureSource().getFeatures(
                    filter);
            Iterator iter = col.iterator();
            int i = 0;
            System.out.println("Spliting river into parts. Count = "
                    + col.size());
            while (iter.hasNext())
            {
                SimpleFeature feature = (SimpleFeature) iter.next();
                System.out.println(feature.getDefaultGeometry().getClass()
                        .getName());
                Geometry curGeom = (Geometry) feature.getDefaultGeometry();
                Geometry res = curGeom.intersection(riverGeom.buffer(1));

                double length = res.getLength();
                double high = (Double) feature.getProperty("Aukstis")
                        .getValue();
                System.out.println("River part: len = " + length + " high = "
                        + high);
                SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(
                        FLAG);

                featureBuilder.add(res);
                featureBuilder.add(i);
                featureBuilder.add("RiverPart" + i);
                featureBuilder.add(length);
                featureBuilder.add(high);
                featureBuilder.add(id);
                SimpleFeature rfeature = featureBuilder
                        .buildFeature("RiverPart." + i);
                list.add(rfeature);
                i++;
            }
        } catch (Exception e)
        {
            e.printStackTrace();
        }
        SimpleFeature[] result = new SimpleFeature[list.size()];
        for (int i = 0; i < list.size(); i++)
        {
            result[i] = list.get(i);
        }
        return result;
    }

    private SimpleFeature[] AreasWithAngles(MapLayer surface, MapLayer rivers,
            MapLayer cities, MapLayer areas, double x, double y, double n,
            double hg)
    {
        try
        {
            ArrayList<SimpleFeature> newPlots = new ArrayList<SimpleFeature>();
            SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
            builder.setName("AreaExt");
            builder.setNamespaceURI("http://localhost/");
            builder.setCRS(mapContext.getCoordinateReferenceSystem());
            builder.add("the_geom", Polygon.class);
            builder.setDefaultGeometry("the_geom");
            builder.add("ID", Integer.class);
            builder.add("Name", String.class);
            builder.add("NUOLYDIS_SR", double.class);
            builder.add("NUOLYDIS_PR", double.class);
            builder.add("NUOLYDIS_PV", double.class);
            builder.add("NUOLYDIS_SV", double.class);
            builder.add("GYVVARDAS", String.class);
            builder.add("GYVSK", int.class);
            SimpleFeatureType FLAG = builder.buildFeatureType();
            FeatureCollection col = areas.getFeatureSource().getFeatures();
            ArrayList<SimpleFeature> result = new ArrayList<SimpleFeature>();
            System.out.println(col.size());
            Iterator iter = col.iterator();
            int i = 0;

            double size = 44 / col.size();
            while (iter.hasNext())
            {
                SimpleFeature current = (SimpleFeature) iter.next();

                SimpleFeature cityF = NearestCity(current, cities, n);
                String nearestCityName = "";
                int cityCitizensCount = 0;
                if (cityF != null)
                {
                    nearestCityName = (String) cityF.getProperty("GYVVARDAS")
                            .getValue();

                    cityCitizensCount = (Integer) cityF.getProperty("GYVSK")
                            .getValue();
                }
                SimpleFeature riverF = NearestRiver(cityF, rivers);
                SimpleFeature[] riverParts = RiverParts(riverF, surface);
                SimpleFeature newArea = ExtendArea(current, cityF, riverParts,
                        FLAG, nearestCityName, cityCitizensCount);
                if (newArea != null)
                {
                    updateTitle("Spliting " + i);
                    SimpleFeature[] col1 = this.Split(newArea, x, y, riverF,
                            cityF);
                    for (int k = 0; k < col1.length; k++)
                    {
                        if (col1[k] != null)
                        {
                            newPlots.add(col1[k]);
                        }
                    }

                    result.add(newArea);
                } else
                {
                    System.out.println("Nulas");
                }
                i++;
                updateStatus(56 + (int) (size * i));
                updateTitle("Extending area object " + i);
            }
            SimpleFeature[] fR = new SimpleFeature[result.size()];
            for (int j = 0; j < result.size(); j++)
            {
                fR[j] = result.get(j);
            }
            SimpleFeature[] h = new SimpleFeature[newPlots.size()];
            System.out.println("Split areas: " + newPlots.size());
            if (newPlots.size() > 0)
            {
                for (int k = 0; k < newPlots.size(); k++)
                {
                    h[k] = newPlots.get(k);
                }
                MapLayer tempLayer = this.CreateLayerFromFeatures(h, "Plots",
                        Color.WHITE, Color.red);
                FilterFactory2 ff = CommonFactoryFinder
                        .getFilterFactory2(GeoTools.getDefaultHints());

                org.opengis.filter.Filter filter = ff.greater(ff
                        .property("NUOLYDIS"), ff.literal(hg));
                FeatureCollection<? extends FeatureType, ? extends Feature> features = tempLayer
                        .getFeatureSource().getFeatures(filter);

                mapContext.addLayer(this.CreateLayerFromFeatures(
                        FromCollection(features), "Plots", Color.WHITE,
                        Color.red));

                layersPanel.addLayer();
            } else
            {
                // JOptionPane.showMessageDialog(null, "No results found",
                // "Error", JOptionPane.ERROR_MESSAGE);
            }
            return fR;
        } catch (Exception e)
        {
            e.printStackTrace();
        }

        return new SimpleFeature[0];
    }

    private Geometry createCircle(double x, double y, final double RADIUS)
    {
        /*
         * final int SIDES = 32; Coordinate coords[] = new Coordinate[SIDES+1];
         * for( int i = 0; i < SIDES; i++){ double angle = ((double) i /
         * (double) SIDES) * Math.PI * 2.0; double dx = Math.cos( angle ) *
         * RADIUS; double dy = Math.sin( angle ) * RADIUS; coords[i] = new
         * Coordinate( (double) x + dx, (double) y + dy ); } coords[SIDES] =
         * coords[0];
         * 
         * LinearRing ring = factory.createLinearRing( coords ); Polygon polygon
         * = factory.createPolygon( ring, null );
         * 
         * return polygon;
         */
        GeometryFactory geometryFactory = JTSFactoryFinder
                .getGeometryFactory(null);
        Coordinate coord = new Coordinate(x, y);
        com.vividsolutions.jts.geom.Point point = geometryFactory
                .createPoint(coord);
        return point.buffer(RADIUS);
    }

    private int AngleMode(Geometry areaGeom, Geometry cityGeom)
    {
        Coordinate p1 = areaGeom.getCentroid().getCoordinate();
        Coordinate p2 = cityGeom.getCentroid().getCoordinate();
        int x = (p1.x > p2.x) ? -1 : 1;
        int y = (p1.y > p2.y) ? -1 : 1;
        if ((x > 0) && (y < 0))
        {
            return 0;
        } else if ((x > 0) && (y > 0))
        {
            return 1;
        } else if ((x < 0) && (y > 0))
        {
            return 2;
        } else
        {
            return 3;
        }
    }

    private String PropertyNameByMode(int mode)
    {
        switch (mode)
        {
            case 0:
                return "NUOLYDIS_SR";
            case 1:
                return "NUOLYDIS_PR";
            case 2:
                return "NUOLYDIS_PV";
            case 3:
                return "NUOLYDIS_SV";
        }
        return "";
    }

    private SimpleFeature[] Split(SimpleFeature area, double a, double b,
            SimpleFeature river, SimpleFeature city)
    {
        double r = Math.sqrt(a * a + b * b) / 2;
        double step = 50;
        ArrayList<SimpleFeature> list = new ArrayList<SimpleFeature>();
        BoundingBox bbox = area.getBounds();
        double startX = bbox.getMinX() + r;
        double startY = bbox.getMinY() + r;
        double endX = bbox.getMaxX() - r;
        double endY = bbox.getMaxY() - r;
        double curX = startX;
        double curY = startY;
        double[] angles = new double[4];
        angles[0] = (Double) area.getProperty(PropertyNameByMode(0)).getValue();
        angles[1] = (Double) area.getProperty(PropertyNameByMode(1)).getValue();
        angles[2] = (Double) area.getProperty(PropertyNameByMode(2)).getValue();
        angles[3] = (Double) area.getProperty(PropertyNameByMode(3)).getValue();
        int gyvSk = (Integer) city.getProperty("GYVSK").getValue();
        String gyvVardas = (String) city.getProperty("GYVVARDAS").getValue();
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("PlotExt");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", Polygon.class);
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        builder.add("NUOLYDIS", double.class);
        builder.add("GYVVARDAS", String.class);
        builder.add("GYVSK", int.class);
        builder.add("IKIGYV", double.class);
        builder.add("IKIUPES", double.class);
        builder.add("NUOMIESTOIKIUPES", double.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        Geometry areaGeom = (Geometry) area.getDefaultGeometry();
        Geometry cityGeom = (Geometry) city.getDefaultGeometry();
        double distanceCityRiver = cityGeom.distance((Geometry) river
                .getDefaultGeometry());
        while (curY <= endY)
        {

            Geometry geom = this.createCircle(curX, curY, r);

            double b1 = areaGeom.distance(geom);

            if (areaGeom.contains(geom))
            {
                SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(
                        FLAG);
                int mode = this.AngleMode(geom, cityGeom);
                featureBuilder.add(geom);
                featureBuilder.add(0);
                featureBuilder.add("Plot" + curX + ":" + curY + "Ext");
                featureBuilder.add(angles[mode]);
                featureBuilder.add(gyvVardas);
                featureBuilder.add(gyvSk);
                featureBuilder.add(geom.distance((Geometry) city
                        .getDefaultGeometry()));
                featureBuilder.add(geom.distance((Geometry) river
                        .getDefaultGeometry()));
                featureBuilder.add(distanceCityRiver);
                SimpleFeature rfeature = featureBuilder.buildFeature("PlotExt."
                        + curX + ":" + curY);
                list.add(rfeature);
            }
            curX += step;
            if (curX > endX)
            {
                curX = startX;
                curY += step;
            }
        }
        SimpleFeature[] result = new SimpleFeature[list.size()];
        System.out.println("Total objects: " + list.size());
        for (int i = 0; i < list.size(); i++)
        {
            result[i] = list.get(i);
        }
        return result;
    }

    private SimpleFeature[] SortByVector(int mode, SimpleFeature[] col)
    {
        SimpleFeature[] f = new SimpleFeature[col.length];
        int x = 1;
        int y = 1;
        switch (mode)
        {
            case 0:
                x = 1;
                y = -1;
                break;
            case 1:
                x = 1;
                y = 1;
                break;
            case 2:
                x = -1;
                y = 1;
                break;
            case 3:
                x = -1;
                y = -1;
                break;
        }
        for (int i = 0; i < col.length; i++)
        {
            SimpleFeature fea = col[i];
            Coordinate current = ((Geometry) fea.getDefaultGeometry())
                    .getCentroid().getCoordinate();
            int pos = 0;
            for (int j = 0; j < i; j++)
            {
                Coordinate geom = ((Geometry) f[j].getDefaultGeometry())
                        .getCentroid().getCoordinate();
                if (x * geom.x < x * current.x)
                {
                    pos++;
                } else if ((x * geom.x == x * current.x)
                        && (y * geom.y < y * current.y))
                {
                    pos++;
                } else
                {
                }
            }
            for (int j = i; j > pos; j--)
            {
                f[j] = f[j - 1];
            }
            f[pos] = fea;
        }
        return f;
    }

    private double RiverAngleByMode(int mode, SimpleFeature[] riverParts)
    {
        SimpleFeature[] f = SortByVector(mode, riverParts);
        if (f.length > 0)
        {
            double lastHeight = ((Double) f[0].getProperty("AUKSTIS")
                    .getValue()).doubleValue();
            double lastLength = ((Double) f[0].getProperty("ILGIS").getValue())
                    .doubleValue();
            double sum = 0;
            double fullLength = lastLength;
            for (int j = 1; j < f.length; j++)
            {
                double curHeight = ((Double) f[j].getProperty("AUKSTIS")
                        .getValue()).doubleValue();
                double curLength = ((Double) f[j].getProperty("ILGIS")
                        .getValue()).doubleValue();
                double delta = lastHeight - curHeight;
                sum += curLength * delta;
                fullLength += curLength;
            }
            double vid = sum / fullLength;
            vid = Math.asin(vid / fullLength);
            return vid;
        } else
        {
            return 0;
        }
    }

    private SimpleFeature ExtendArea(SimpleFeature obj1, SimpleFeature obj2,
            SimpleFeature[] col, SimpleFeatureType flag, String cityName,
            double citizensCount)
    {

        Geometry areaGeom = (Geometry) obj1.getProperty("the_geom").getValue();
        Coordinate p1 = ((Geometry) obj1.getDefaultGeometry()).getCentroid()
                .getCoordinate();
        Coordinate p2 = ((Geometry) obj2.getDefaultGeometry()).getCentroid()
                .getCoordinate();
        int x = (p1.x > p2.x) ? -1 : 1;
        int y = (p1.y > p2.y) ? -1 : 1;
        double angle0 = RiverAngleByMode(0, col);
        double angle1 = RiverAngleByMode(1, col);
        double angle2 = RiverAngleByMode(2, col);
        double angle3 = RiverAngleByMode(3, col);
        int id = (Integer) obj1.getProperty("ID").getValue();
        String name = (String) obj1.getProperty("Name").getValue();
        SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(flag);

        featureBuilder.add(areaGeom);
        featureBuilder.add(id);
        featureBuilder.add(name + "Ext");
        featureBuilder.add(angle0);
        featureBuilder.add(angle1);
        featureBuilder.add(angle2);
        featureBuilder.add(angle3);
        featureBuilder.add(cityName);
        featureBuilder.add(citizensCount);
        SimpleFeature rfeature = featureBuilder.buildFeature("AreaExt." + id);
        return rfeature;
    }

    private void SpecialFunctionWorker(final SimpleFeature SelectedDistrict,
            final double x, final double y, final double m, final double n,
            final double z, final double k, final double h, final String c1,
            final String c2, final String c3)
    {
        initStatus("Iniciating layers", "Looking for possible areas", 100);
        try
        {
            boolean toContinue = true;
            SimpleFeature[] roadsA = null;
            SimpleFeature[] citiesA = null;
            SimpleFeature[] citiesAll = null;
            SimpleFeature[] woodsA = null;
            SimpleFeature[] lakesA = null;
            SimpleFeature[] peaksA = null;
            SimpleFeature[] riversA = null;
            SimpleFeature riversArea = null;
            SimpleFeature riversBasein = null;
            MapLayer citiesLayer = null;
            MapLayer surfaceLayer = null;
            MapLayer riversLayer = null;
            MapLayer areaLayer = null;
            try
            {
                this.removeDefLayer("Lakes");
                this.removeDefLayer("Peaks");
                this.removeDefLayer("Roads");
                this.removeDefLayer("Woods");
                this.removeDefLayer("Rivers");
                this.removeDefLayer("PossibleArea");
                this.removeDefLayer("Cities");
                this.removeDefLayer("Surface");
                this.removeDefLayer("Plots");
                this.removeDefLayer("ExtendedArea");
                updateStatus(2);
                updateTitle("Loading roads layer");
                this.highlightTidyUp();
                MapLayer roads = addDefLayer("keliai.shp");
                updateStatus(4);
                updateTitle("Filtering roads layer");
                FilterFactory2 ff = CommonFactoryFinder
                        .getFilterFactory2(GeoTools.getDefaultHints());
                org.opengis.filter.Filter filter = ff.dwithin(ff
                        .property("the_geom"), ff.literal(SelectedDistrict
                        .getDefaultGeometry()), 0, "");
                FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                        .getFeatureSource().getFeatures(filter);

                roadsA = FromCollection(features);
                if ((roadsA == null) || (roadsA.length == 0))
                {
                    // toContinue = false;
                    // JOptionPane.showMessageDialog(null,
                    // "No needed cities found. Can't continue", "Error",
                    // JOptionPane.ERROR_MESSAGE);
                } else
                {
                    MapLayer layer = CreateLayerFromFeatures(roadsA, "Roads",
                            Color.BLACK, Color.BLACK);
                    mapContext.addLayer(layer);
                    layersPanel.addLayer();
                    updateStatus(6);
                    updateTitle("Adding new roads layer");
                }
            } catch (Exception e)
            {
                updateTitle("Error " + e.getMessage());
                e.printStackTrace();
            }
            updateStatus(8);
            updateTitle("Removing old roads layer");
            // this.removeDefLayer("keliai.shp");
            this.repaint();
            if (toContinue)
            {
                try
                {
                    updateStatus(10);
                    updateTitle("Loading cities layer");
                    MapLayer roads = addDefLayer("gyvenvie.shp");
                    updateStatus(12);
                    updateTitle("Filtering cities layer");
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.dwithin(ff
                            .property("the_geom"), ff.literal(SelectedDistrict
                            .getDefaultGeometry()), 0, "");
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                            .getFeatureSource().getFeatures(filter);
                    citiesAll = FromCollection(roads.getFeatureSource()
                            .getFeatures());
                    MapLayer layer = this
                            .CreateLayerFromFeatures(ExtendCities(FromCollection(features)));
                    filter = ff.and(ff.and(ff.greater(ff.property("GYVSK"), ff
                            .literal(z)), ff.less(ff.property("GYVSK"), ff
                            .literal(k))),
                            ff
                                    .not(CQL.toFilter("(GYVVARDAS like '%" + c1
                                            + "%') OR (GYVVARDAS like '%" + c2
                                            + "%') OR (GYVVARDAS like '%" + c3
                                            + "%')")));
                    features = layer.getFeatureSource().getFeatures(filter);
                    citiesA = FromCollection(features);
                    if ((citiesA == null) || (citiesA.length == 0))
                    {
                        toContinue = false;
                        JOptionPane.showMessageDialog(null,
                                "No needed cities found. Can't continue",
                                "Error", JOptionPane.ERROR_MESSAGE);
                    } else
                    {

                        riversArea = this.PossibleRiversArea(citiesA);
                        layer = this.CreateLayerFromFeatures(citiesA, "Cities",
                                Color.red, Color.red);
                        citiesLayer = layer;
                        // mapContext.addLayer(layer);
                        updateStatus(14);
                        updateTitle("Adding new cities layer");
                    }
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                updateStatus(16);
                updateTitle("Removing old cities layer");
                this.repaint();
            }
            // this.removeDefLayer("gyvenvie.shp");
            if (toContinue)
            {
                try
                {
                    updateStatus(18);
                    updateTitle("Loading woods layer");
                    MapLayer roads = addDefLayer("miskai.shp");
                    updateStatus(20);
                    updateTitle("Filtering woods layer");
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.dwithin(ff
                            .property("the_geom"), ff.literal(SelectedDistrict
                            .getDefaultGeometry()), 0, "");
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                            .getFeatureSource().getFeatures(filter);
                    MapLayer layer = CreateLayerFromFeatures(ExtendWoods(FromCollection(features)));
                    filter = ff.greater(ff.property("Plotas"), ff.literal(100));
                    features = layer.getFeatureSource().getFeatures(filter);
                    woodsA = FromCollection(features);
                    layer = CreateLayerFromFeatures(woodsA, "Woods",
                            Color.BLACK, Color.GREEN);
                    mapContext.addLayer(layer);
                    layersPanel.addLayer();
                    updateStatus(22);
                    updateTitle("Adding new woods layer");
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                updateStatus(24);
                updateTitle("Removing old woods layer");
                this.repaint();
            }
            // this.removeDefLayer("miskai.shp");
            if (toContinue)
            {
                try
                {
                    updateStatus(26);
                    updateTitle("Loading lakes layer");
                    MapLayer roads = addDefLayer("ezerai.shp");
                    updateStatus(28);
                    updateTitle("Filtering lakes layer");
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.dwithin(ff
                            .property("the_geom"), ff.literal(SelectedDistrict
                            .getDefaultGeometry()), 0, "");
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                            .getFeatureSource().getFeatures(filter);
                    lakesA = FromCollection(features);
                    if ((lakesA != null) && (lakesA.length > 0))
                    {
                        MapLayer layer = this.CreateLayerFromFeatures(lakesA,
                                "Lakes", Color.BLACK, Color.BLUE);

                        mapContext.addLayer(layer);
                        layersPanel.addLayer();
                    }
                    updateStatus(30);
                    updateTitle("Adding new lakes layer");
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                updateStatus(32);
                updateTitle("Removing old lakes layer");
                this.repaint();
            }
            if (toContinue)
            {
                // this.removeDefLayer("ezerai.shp");
                try
                {
                    updateStatus(34);
                    updateTitle("Loading peaks layer");
                    MapLayer roads = addDefLayer("virsukal.shp");
                    updateStatus(36);
                    updateTitle("Filtering peaks layer");
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.and(ff.dwithin(ff
                            .property("the_geom"), ff.literal(SelectedDistrict
                            .getDefaultGeometry()), 0, ""), ff.greater(ff
                            .property("AUKSTIS"), ff.literal(500)));
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                            .getFeatureSource().getFeatures(filter);
                    peaksA = FromCollection(features);
                    if ((peaksA != null) && (peaksA.length > 0))
                    {
                        MapLayer layer = this.CreateLayerFromFeatures(peaksA,
                                "Peaks", Color.yellow, Color.yellow);
                        mapContext.addLayer(layer);
                        layersPanel.addLayer();
                    }
                    updateStatus(38);
                    updateTitle("Adding new peaks layer");
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                updateStatus(40);
                updateTitle("Removing old peaks layer");
                this.repaint();
            }
            // this.removeDefLayer("virsukal.shp");
            if (toContinue)
            {
                try
                {
                    updateStatus(42);
                    updateTitle("Loading rivers layer");
                    MapLayer roads = addDefLayer("upes.shp");
                    updateStatus(44);
                    updateTitle("Filtering rivers layer");
                    if (riversArea != null)
                    {
                        FilterFactory2 ff = CommonFactoryFinder
                                .getFilterFactory2(GeoTools.getDefaultHints());
                        org.opengis.filter.Filter filter = ff.and(ff.dwithin(ff
                                .property("the_geom"),
                                ff.literal(SelectedDistrict
                                        .getDefaultGeometry()), 0, ""), ff
                                .dwithin(ff.property("the_geom"), ff
                                        .literal(riversArea
                                                .getDefaultGeometry()), 0, ""));
                        FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                                .getFeatureSource().getFeatures(filter);
                        riversA = FromCollection(features);
                        if ((riversA != null) && (riversA.length > 0))
                        {
                            riversBasein = this.RiversBasein(riversA);
                            MapLayer layer = this.CreateLayerFromFeatures(
                                    riversA, "Rivers", Color.yellow,
                                    Color.yellow);
                            riversLayer = layer;
                            mapContext.addLayer(layer);
                            layersPanel.addLayer();
                        } else
                        {
                            toContinue = false;
                            JOptionPane.showMessageDialog(null,
                                    "No needed rivers found", "Error",
                                    JOptionPane.ERROR_MESSAGE);
                        }
                    } else
                    {
                        toContinue = false;
                        JOptionPane.showMessageDialog(null,
                                "No needed rivers found", "Error",
                                JOptionPane.ERROR_MESSAGE);
                    }
                    updateStatus(46);
                    updateTitle("Adding new rivers layer");
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                updateStatus(48);
                updateTitle("Removing old rivers layer");
                this.repaint();
            }
            // this.removeDefLayer("upes.shp");
            if (toContinue)
            {
                try
                {
                    updateStatus(50);
                    updateTitle("Creating possible area layer");
                    SimpleFeature[] a = PossibleAreas(SelectedDistrict,
                            citiesA, woodsA, lakesA, peaksA, roadsA, riversA,
                            riversBasein, x, y, m, n, citiesAll);
                    MapLayer layer = this.CreateLayerFromFeatures(a,
                            "PossibleArea", Color.red, Color.red);
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.greater(ff
                            .property("Plotas"), ff.literal(x * y));
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = layer
                            .getFeatureSource().getFeatures(filter);
                    a = FromCollection(features);
                    layer = this.CreateLayerFromFeatures(a, "PossibleArea",
                            Color.red, Color.red);
                    areaLayer = layer;
                    mapContext.addLayer(layer);
                    layersPanel.addLayer();
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
                this.repaint();
            }
            if (toContinue)
            {
                try
                {
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.dwithin(ff
                            .property("the_geom"), ff.literal(riversBasein
                            .getDefaultGeometry()), 0, "");
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = citiesLayer
                            .getFeatureSource().getFeatures(filter);
                    // citiesLayer =
                    citiesA = FromCollection(features);

                    citiesLayer = this.CreateLayerFromFeatures(citiesA,
                            "Cities", Color.red, Color.red);
                    mapContext.addLayer(citiesLayer);
                    layersPanel.addLayer();
                } catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
            if (toContinue)
            {
                SimpleFeature citiesAreaForPav = CitiesArea(citiesA, n);
                try
                {
                    updateStatus(52);
                    updateTitle("Loading surface layer");
                    MapLayer roads = addDefLayer("pavirs_lt_p.shp");
                    updateStatus(54);
                    updateTitle("Filtering surface layer");
                    FilterFactory2 ff = CommonFactoryFinder
                            .getFilterFactory2(GeoTools.getDefaultHints());
                    org.opengis.filter.Filter filter = ff.dwithin(ff
                            .property("the_geom"), ff.literal(citiesAreaForPav
                            .getDefaultGeometry()), 0, "");
                    FeatureCollection<? extends FeatureType, ? extends Feature> features = roads
                            .getFeatureSource().getFeatures(filter);
                    // riversA = FromCollection(features);
                    // riversBasein = this.RiversBasein(riversA);
                    MapLayer layer = this.CreateLayerFromFeatures(
                            FromCollection(features), "Surface", Color.black,
                            Color.white);
                    surfaceLayer = layer;
                    mapContext.addLayer(layer);
                    layersPanel.addLayer();
                    updateStatus(56);
                    updateTitle("Adding new surface layer");
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }
            }
            if (toContinue)
            {
                try
                {
                    SimpleFeature[] feat = AreasWithAngles(surfaceLayer,
                            riversLayer, citiesLayer, areaLayer, x, y, n, h);
                    MapLayer layer = this.CreateLayerFromFeatures(feat,
                            "ExtendedArea", Color.white, Color.yellow);
                    mapContext.addLayer(layer);
                    layersPanel.addLayer();
                } catch (Exception e)
                {
                    updateTitle("Error " + e.getMessage());
                    e.printStackTrace();
                }

                this.removeDefLayer("Lakes");
                this.removeDefLayer("Peaks");
                this.removeDefLayer("Roads");
                this.removeDefLayer("Woods");
                this.repaint();
                if (toContinue)
                {
                    int index = this.findLayerNo("Plots");
                    if (index < 0)
                    {
                        JOptionPane.showMessageDialog(null, "No results",
                                "Error", JOptionPane.WARNING_MESSAGE);
                    } else
                    {
                        this.setActiveLayer(index);
                        this.selectAll();
                        this.zoomSel();
                        this.repaint();
                    }
                }
                for (int i = 1; i < mapContext.getLayerCount(); i++)
                {
                    if (!mapContext.getLayer(i).getTitle().equals("Plots"))
                    {
                        mapContext.getLayer(i).setVisible(false);
                    }
                }

            }
        } catch (Exception ee)
        {
            JOptionPane.showMessageDialog(null, "No results", "Error",
                    JOptionPane.WARNING_MESSAGE);
        } finally
        {
            dialog.setVisible(false);
            if (this.wind != null)
            {
                wind.upPanel();
            }
        }

    }

    private void fetchRecord(int index)
    {
        try
        {
            Thread.sleep(1000);
        } catch (InterruptedException e)
        {
            e.printStackTrace();
        }
    }

    public boolean search(String cqlSentence, boolean showMessage)
    {
        boolean ok = true;
        successSelectByAttribute = true;
        table.setModel(new DefaultTableModel(new Object[0][0],
                new String[] { "Value" }));
        MapLayer queryLayer = mapContext.getLayer(getActiveLayer());
        if (queryLayer == highlightLayer)
        {
            queryLayer = mapContext.getLayer(mapContext.getLayerCount() - 2);
        }
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        org.opengis.filter.Filter filter = null;
        try
        {
            filter = CQL.toFilter(cqlSentence);
        } catch (CQLException e)
        {
            // e.printStackTrace();
            try
            {
                ok = false;
                filter = CQL.toFilter("1 <> 1");
            } catch (CQLException e2)
            {
                if (showMessage)
                {
                    JOptionPane.showMessageDialog(null, e2.getMessage(),
                            "Error", JOptionPane.WARNING_MESSAGE);
                } else
                {
                    successSelectByAttribute = false;
                }
            }
            if (showMessage)
            {
                JOptionPane.showMessageDialog(null, e.getMessage(), "Error",
                        JOptionPane.WARNING_MESSAGE);
            }
        }
        if (ok)
        {
            try
            {
                FeatureSource fs = queryLayer.getFeatureSource();
                FeatureCollection fr = fs.getFeatures(filter);

                if (fr != null)
                {

                    if ((fr.size() >= 1))
                    {
                        Iterator<SimpleFeature> reader = (Iterator<SimpleFeature>) fr
                                .iterator();
                        SimpleFeature f = null;
                        ArrayList<SimpleFeature> fArr = new ArrayList<SimpleFeature>();
                        while (reader.hasNext())
                        {
                            f = reader.next();
                            fArr.add(f);
                        }
                        SimpleFeature[] fArr2 = new SimpleFeature[fArr.size()];
                        for (int i = 0; i < fArr.size(); i++)
                        {
                            fArr2[i] = fArr.get(i);
                        }
                        highlightFeatures(fArr2);
                        this.repaint();
                    } else
                    {
                        if (showMessage)
                        {
                            JOptionPane.showMessageDialog(null,
                                    "No objects found", "Info",
                                    JOptionPane.WARNING_MESSAGE);
                        } else
                        {
                            successSelectByAttribute = false;
                        }

                    }
                } else
                {
                    if (showMessage)
                    {
                        JOptionPane.showMessageDialog(null, "No objects found",
                                "Info", JOptionPane.WARNING_MESSAGE);
                    } else
                    {
                        successSelectByAttribute = false;
                    }

                }
            } catch (Exception e1)
            {
                if (showMessage)
                {
                    e1.printStackTrace();
                    JOptionPane.showMessageDialog(null, "Error", e1
                            .getMessage(), JOptionPane.WARNING_MESSAGE);
                } else
                {
                    successSelectByAttribute = false;
                }

            }
        } else
        {
            successSelectByAttribute = false;
            highlightFeatures(new SimpleFeature[0]);
            this.repaint();
        }
        return ok;
    }

    DefaultMapLayer highlightLayer = null;
    MemoryDataStore highlight = null;

    private SimpleFeature[] FromCollection(FeatureCollection collection)
    {
        SimpleFeature[] result = new SimpleFeature[collection.size()];
        Iterator<SimpleFeature> iter = collection.iterator();
        int i = 0;
        while (iter.hasNext())
        {
            result[i] = iter.next();
            i++;
        }
        return result;
    }

    private GeometryCollection FromFeatures(SimpleFeature[] data,
            GeometryFactory factory)
    {

        Geometry[] geometries = new Geometry[data.length];
        if (data != null)
        {
            for (int i = 0; i < data.length; i++)
            {
                geometries[i] = (Geometry) data[i].getDefaultGeometry();
            }
            GeometryCollection geometryCollection = new GeometryCollection(
                    geometries, factory);

            return geometryCollection;
        } else
        {
            return null;
        }
    }

    private SimpleFeature RiversBasein(SimpleFeature[] rivers)
    {
        GeometryFactory factory = JTSFactoryFinder.getGeometryFactory(null);
        Geometry miestaiGeom = FromFeatures(rivers, factory);
        Geometry plot = miestaiGeom.buffer(1000);
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("SimpleArea");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", plot.getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
        featureBuilder.add(plot);
        featureBuilder.add(0);
        featureBuilder.add("Area");
        SimpleFeature feature = featureBuilder.buildFeature("Area.0");
        return feature;

    }

    private SimpleFeature PossibleRiversArea(SimpleFeature[] miestai)
    {
        GeometryFactory factory = FactoryFinder.getGeometryFactory(null);
        Geometry miestaiGeom = FromFeatures(miestai, factory);
        Geometry plot = miestaiGeom.buffer(1000);
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("SimpleArea");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", plot.getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
        featureBuilder.add(plot);
        featureBuilder.add(0);
        featureBuilder.add("Area");
        SimpleFeature feature = featureBuilder.buildFeature("Area.0");
        return feature;
    }

    private SimpleFeature CitiesArea(SimpleFeature[] miestai, double radius)
    {
        GeometryFactory factory = FactoryFinder.getGeometryFactory(null);
        Geometry miestaiGeom = FromFeatures(miestai, factory);
        Geometry plot = miestaiGeom.buffer(radius);
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("SimpleArea");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", plot.getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
        featureBuilder.add(plot);
        featureBuilder.add(0);
        featureBuilder.add("Area");
        SimpleFeature feature = featureBuilder.buildFeature("Area.0");
        return feature;
    }

    private SimpleFeature[] PossibleAreas(SimpleFeature apskritis,
            SimpleFeature[] miestai, SimpleFeature[] miskai,
            SimpleFeature[] ezerai, SimpleFeature[] virsukalnes,
            SimpleFeature[] keliai, SimpleFeature[] upes,
            SimpleFeature riversBasein, double a, double b, double m,
            double ng, SimpleFeature[] citiesAll)
    {
        GeometryFactory factory = FactoryFinder.getGeometryFactory(null);
        Geometry miestaiGeom = FromFeatures(miestai, factory);
        Geometry citiesAllGeom = FromFeatures(citiesAll, factory).buffer(m);
        Geometry plot = miestaiGeom.buffer(ng).difference(citiesAllGeom);
        double r = Math.sqrt(a * a + b * b);
        Geometry basein = FromFeatures(upes, factory).buffer(100 + r);
        plot = plot.difference(miestaiGeom.buffer(m));
        if ((ezerai != null) && (ezerai.length > 0))
        {
            plot = plot.difference(FromFeatures(ezerai, factory).buffer(0));
        }
        if ((virsukalnes != null) && (virsukalnes.length > 0))
        {
            plot = plot.difference(FromFeatures(virsukalnes, factory).buffer(
                    100));
        }
        if ((keliai != null) && (keliai.length > 0))
        {
            plot = plot.difference(FromFeatures(keliai, factory).buffer(100));
        }
        if ((keliai != null) && (keliai.length > 0))
        {
            plot = plot.difference(FromFeatures(miskai, factory).buffer(0));
        }
        plot = plot.intersection(basein);
        FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(GeoTools
                .getDefaultHints());
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("SimpleArea");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", plot.getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Name", String.class);
        builder.add("Plotas", double.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        int n = plot.getNumGeometries();
        SimpleFeature result[] = new SimpleFeature[n];
        for (int i = 0; i < n; i++)
        {
            SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
            featureBuilder.add(plot.getGeometryN(i));
            featureBuilder.add(i);
            featureBuilder.add("Area");
            featureBuilder.add(plot.getGeometryN(i).getArea());
            SimpleFeature feature = featureBuilder.buildFeature("Area." + i);
            result[i] = feature;
        }
        return result;
    }

    /*
     * private SimpleFeature PossibleArea(SimpleFeature apskritis,
     * SimpleFeature[] miestai, SimpleFeature[] miskai, SimpleFeature[] ezerai,
     * SimpleFeature[] virsukalnes, SimpleFeature[] keliai, SimpleFeature[]
     * upes, int m, int n) { GeometryFactory factory =
     * FactoryFinder.getGeometryFactory(null); Geometry apskritisGeom =
     * (Geometry) apskritis.getDefaultGeometry(); Geometry miestaiGeom =
     * FromFeatures(miestai, factory); Geometry plot = miestaiGeom.buffer(n);
     * plot = plot.difference(miestaiGeom.buffer(m)); plot =
     * plot.difference(FromFeatures(ezerai, factory).buffer(0)); plot =
     * plot.difference(FromFeatures(virsukalnes, factory).buffer(100)); plot =
     * plot.difference(FromFeatures(keliai, factory).buffer(100)); plot =
     * plot.difference(FromFeatures(miskai, factory).buffer(0)); FilterFactory2
     * ff = CommonFactoryFinder.getFilterFactory2(GeoTools .getDefaultHints());
     * SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
     * builder.setName("SimpleArea");
     * builder.setNamespaceURI("http://localhost/");
     * builder.setCRS(mapContext.getCoordinateReferenceSystem());
     * builder.add("the_geom", plot.getClass());
     * builder.setDefaultGeometry("the_geom"); builder.add("ID", Integer.class);
     * builder.add("Name", String.class); SimpleFeatureType FLAG =
     * builder.buildFeatureType(); SimpleFeatureBuilder featureBuilder = new
     * SimpleFeatureBuilder(FLAG); featureBuilder.add(plot);
     * featureBuilder.add(0); featureBuilder.add("Area"); SimpleFeature feature
     * = featureBuilder.buildFeature("Area.1"); return feature; }
     */
    private SimpleFeature[] ExtendCities(SimpleFeature[] cities)
    {
        SimpleFeature[] result = new SimpleFeature[cities.length];
        if (cities.length > 0)
        {
            SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
            builder.setName("Miestas");
            builder.setNamespaceURI("http://localhost/");
            builder.setCRS(mapContext.getCoordinateReferenceSystem());
            builder.add("the_geom", cities[0].getDefaultGeometry().getClass());
            builder.setDefaultGeometry("the_geom");
            builder.add("ID", Integer.class);
            builder.add("GYVSK", int.class);
            builder.add("GYVVARDAS", String.class);
            SimpleFeatureType FLAG = builder.buildFeatureType();
            SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
            Random rnd = new Random();
            for (int i = 0; i < result.length; i++)
            {
                Geometry geom = (Geometry) cities[i].getDefaultGeometry();
                featureBuilder.add(geom);
                featureBuilder.add(i);
                String GyvVardas = (String) cities[i].getAttribute("GYVVARDAS");
                int sk = ((GyvVardas.codePointAt(0) * 10 + GyvVardas
                        .codePointAt(1)) * 10 + GyvVardas.codePointAt(3))
                        * 10 + rnd.nextInt(1000);
                // featureBuilder.add(rnd.nextInt(100000) + 10000);
                featureBuilder.add(sk);
                featureBuilder.add(cities[i].getAttribute("GYVVARDAS"));
                SimpleFeature feature = featureBuilder.buildFeature("Miestas."
                        + i);
                result[i] = feature;
            }
        }
        return result;
    }

    private SimpleFeature[] ExtendWoods(SimpleFeature[] woods)
    {
        SimpleFeature[] result = new SimpleFeature[woods.length];
        SimpleFeatureTypeBuilder builder = new SimpleFeatureTypeBuilder();
        builder.setName("Miskas");
        builder.setNamespaceURI("http://localhost/");
        builder.setCRS(mapContext.getCoordinateReferenceSystem());
        builder.add("the_geom", woods[0].getDefaultGeometry().getClass());
        builder.setDefaultGeometry("the_geom");
        builder.add("ID", Integer.class);
        builder.add("Plotas", double.class);
        SimpleFeatureType FLAG = builder.buildFeatureType();
        SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
        for (int i = 0; i < result.length; i++)
        {
            Geometry geom = (Geometry) woods[i].getDefaultGeometry();
            featureBuilder.add(geom);
            featureBuilder.add(i);
            featureBuilder.add(geom.getArea());
            SimpleFeature feature = featureBuilder.buildFeature("Miskas." + i);
            result[i] = feature;
        }
        return result;
    }

    /*
     * private SimpleFeature InitPossibleArea(DefaultMapLayer apskritys,
     * DefaultMapLayer miestai, DefaultMapLayer miskai, DefaultMapLayer ezerai,
     * DefaultMapLayer virsukalnes, DefaultMapLayer keliai, DefaultMapLayer
     * upes) {
     * 
     * return PossibleArea( GetFeaturesArray(apskritys, "APSVARDAS = 'bla'")[0],
     * ExtendCities(GetFeaturesArray(miestai,
     * "OBJECTID > 0 AND (GYVVARDAS NOT LIKE '%a%')")),
     * ExtendWoods(GetFeaturesArray(miskai, "OBJECID > 100")),
     * GetFeaturesArray(ezerai, "OBJECTID > 0"), GetFeaturesArray( virsukalnes,
     * "AUKSTIS > 100"), GetFeaturesArray(keliai, "OBJECTID > 0"),
     * GetFeaturesArray(upes, "OBJECTID > 0")); }
     * 
     * protected SimpleFeature[] test(SimpleFeature fArr[], double radius) {
     * GeometryFactory factory = FactoryFinder.getGeometryFactory(null);
     * Geometry[] geometries = new Geometry[fArr.length];
     * System.out.println("Input name: " +
     * fArr[0].getAttribute("the_geom").getClass().getName()); for (int i = 0; i
     * < fArr.length; i++) { geometries[i] = (Geometry)
     * fArr[i].getDefaultGeometry(); } GeometryCollection geometryCollection =
     * new GeometryCollection( geometries, factory); Geometry union =
     * geometryCollection.buffer(radius); SimpleFeatureTypeBuilder builder = new
     * SimpleFeatureTypeBuilder(); builder.setName("Flag");
     * builder.setNamespaceURI("http://localhost/"); try {
     * builder.setCRS(CRS.decode("EPSG:2600")); } catch
     * (NoSuchAuthorityCodeException e) { e.printStackTrace(); } catch
     * (FactoryException e) { // TODO Auto-generated catch block
     * e.printStackTrace(); } // System.out.println("Geom: " + union.toText());
     * int i = union.getNumGeometries(); System.out.println("NumOfGeometries: "
     * + i); SimpleFeature[] features = new SimpleFeature[i];
     * System.out.println("Class name: " +
     * union.getGeometryN(0).getClass().getName().toString());
     * builder.add("the_geom", Polygon.class);
     * builder.setDefaultGeometry("the_geom"); builder.add("ID", Integer.class);
     * builder.add("Name", String.class); // build the type SimpleFeatureType
     * FLAG = builder.buildFeatureType(); for (int j = 0; j < i; j++) {
     * SimpleFeatureBuilder featureBuilder = new SimpleFeatureBuilder(FLAG);
     * 
     * featureBuilder.add(union.getGeometryN(j)); featureBuilder.add(j);
     * featureBuilder.add("Flag" + j); SimpleFeature feature =
     * featureBuilder.buildFeature("Flag." + j); features[j] = feature; } //
     * AttributeType geom = // AttributeTypeFactory.newAttributeType("the_geom",
     * Polygon.class); return features; }
     * 
     * public void testing() { org.opengis.filter.Filter filter = null; try {
     * filter = CQL.toFilter(
     * "OBJECTID > 0 AND (GYVENV_ID >= 12000) AND (GYVENV_ID <= 1000000) AND (GYVVARDAS NOT LIKE '%a%')"
     * ); FeatureSource fs = mapContext.getLayer(getActiveLayer())
     * .getFeatureSource(); try { FeatureCollection fr = fs.getFeatures(filter);
     * System.out.println("GetFeatureSource"); Iterator<SimpleFeature> reader =
     * (Iterator<SimpleFeature>) fr .iterator();
     * System.out.println("GetReader"); SimpleFeature f = null;
     * ArrayList<SimpleFeature> fArr = new ArrayList<SimpleFeature>(); while
     * (reader.hasNext()) { f = reader.next(); fArr.add(f); } SimpleFeature[]
     * fArr2 = new SimpleFeature[fArr.size()]; for (int i = 0; i < fArr.size();
     * i++) { fArr2[i] = fArr.get(i); } System.out.println("Formed array " +
     * fArr.size()); SimpleFeature[] fArr3 = test(fArr2, 1000); DefaultMapLayer
     * ml = CreateLayerFromFeatures(fArr3); System.out.println("Created layer");
     * getMapContext().addLayer(ml); ml.setTitle("buffer1");
     * ml.setVisible(true); System.out.println("Added layer"); this.repaint();
     * layersPanel.addLayer(); } catch (IOException e) { // TODO Auto-generated
     * catch block e.printStackTrace(); } } catch (CQLException e) { // TODO
     * Auto-generated catch block e.printStackTrace(); }
     * 
     * }
     */
    public SimpleFeature[] GetFeaturesArray(DefaultMapLayer layer,
            String sqlFilter)
    {
        org.opengis.filter.Filter filter = null;
        try
        {
            filter = CQL.toFilter(sqlFilter);
            FeatureSource fs = layer.getFeatureSource();
            try
            {
                FeatureCollection fr = fs.getFeatures(filter);
                Iterator<SimpleFeature> reader = (Iterator<SimpleFeature>) fr
                        .iterator();
                SimpleFeature f = null;
                ArrayList<SimpleFeature> fArr = new ArrayList<SimpleFeature>();
                while (reader.hasNext())
                {
                    f = reader.next();
                    fArr.add(f);
                }
                SimpleFeature[] fArr2 = new SimpleFeature[fArr.size()];
                for (int i = 0; i < fArr.size(); i++)
                {
                    fArr2[i] = fArr.get(i);
                }
                return fArr2;
            } catch (IOException e)
            {
                e.printStackTrace();
            }
        } catch (CQLException e)
        {
            e.printStackTrace();
        }
        return new SimpleFeature[0];
    }

    public DefaultMapLayer CreateLayerFromFeatures(SimpleFeature fArr[],
            String title, Color c1, Color c2)
    {
        if ((fArr != null) && (fArr.length > 0))
        {
            MemoryDataStore store = new MemoryDataStore();
            DefaultMapLayer layer = null;
            if (fArr.length > 0)
            {
                if (fArr[0] != null)
                {
                    store.addFeatures(fArr);
                    FeatureSource<SimpleFeatureType, SimpleFeature> fs = null;
                    try
                    {
                        fs = store.getFeatureSource(fArr[0].getFeatureType()
                                .getName());
                    } catch (IOException e)
                    {
                        e.printStackTrace();
                    }
                    StyleBuilder sb = new StyleBuilder();
                    Class<?> type;
                    try
                    {
                        type = fs.getSchema().getGeometryDescriptor().getType()
                                .getBinding();
                        if (type.isAssignableFrom(Polygon.class)
                                || type.isAssignableFrom(MultiPolygon.class))
                        {
                            PolygonSymbolizer sym = sb.createPolygonSymbolizer(
                                    c2, c1, 3);
                            sym.getFill();
                            Style sty = sb.createStyle(sym);
                            layer = new DefaultMapLayer(fs, sty);
                        } else if (type.isAssignableFrom(LineString.class)
                                || type.isAssignableFrom(MultiLineString.class))
                        {
                            LineSymbolizer sym = sb.createLineSymbolizer(c2, 3);
                            Style sty = sb.createStyle(sym);
                            layer = new DefaultMapLayer(fs, sty);

                        } else
                        {
                            Mark circle = sb.createMark(
                                    StyleBuilder.MARK_CIRCLE, c1);
                            Graphic graph2 = sb.createGraphic(null, circle,
                                    null, 1, 6, 0);
                            PointSymbolizer sym = sb
                                    .createPointSymbolizer(graph2);
                            Style sty = sb.createStyle(sym);
                            layer = new DefaultMapLayer(fs, sty);
                        }
                    } catch (Exception e)
                    {
                        e.printStackTrace();
                    }
                }
            }
            layer.setTitle(title);
            return layer;
        } else
        {
            return null;
        }
    }

    public DefaultMapLayer CreateLayerFromFeatures(SimpleFeature fArr[])
    {
        return CreateLayerFromFeatures(fArr, "", Color.BLACK, Color.YELLOW);
    }

    protected void highlightFeatures(SimpleFeature fArr[])
    {
        this.outputToTable(fArr);
        if (mapContext.indexOf(highlightLayer) >= 0)
        {
            mapContext.removeLayer(highlightLayer);
            highlightLayer = null;
        }
        highlight = null;
        highlight = new MemoryDataStore();

        if (fArr.length > 0)
        {
            if (fArr[0] != null)
            {
                highlight.addFeatures(fArr);

                FeatureSource<SimpleFeatureType, SimpleFeature> fs = null;
                try
                {
                    fs = highlight.getFeatureSource(fArr[0].getFeatureType()
                            .getName());
                } catch (IOException e)
                {
                    e.printStackTrace();
                }
                StyleBuilder sb = new StyleBuilder();
                Class<?> type;
                try
                {
                    type = mapContext.getLayer(getActiveLayer())
                            .getFeatureSource().getSchema()
                            .getGeometryDescriptor().getType().getBinding();
                    // type =
                    // highlight.getSchema(fArr[0].getType().getName()).getGeometryDescriptor().getType().getBinding();
                    // Class<?> type =
                    // highlight.getSchema().getGeometryDescriptor().getType().getBinding();
                    if (type.isAssignableFrom(Polygon.class)
                            || type.isAssignableFrom(MultiPolygon.class))
                    {
                        PolygonSymbolizer sym = sb.createPolygonSymbolizer(
                                Color.YELLOW, Color.BLACK, 3);
                        sym.getFill();
                        Style sty = sb.createStyle(sym);
                        highlightLayer = new DefaultMapLayer(fs, sty);

                    } else if (type.isAssignableFrom(LineString.class)
                            || type.isAssignableFrom(MultiLineString.class))
                    {
                        LineSymbolizer sym = sb.createLineSymbolizer(
                                Color.YELLOW, 3);
                        Style sty = sb.createStyle(sym);
                        highlightLayer = new DefaultMapLayer(fs, sty);

                    } else
                    {
                        PointSymbolizer sym = sb.createPointSymbolizer();
                        Style sty = sb.createStyle(sym);
                        highlightLayer = new DefaultMapLayer(fs, sty);
                    }

                    // PolygonSymbolizer sym =
                    // sb.createPolygonSymbolizer(Color.YELLOW,Color.BLACK,3);
                    // sym.getFill();
                    // Style sty = sb.createStyle(sym);
                    // highlightLayer = new DefaultMapLayer(fs, sty);

                    mapContext.addLayer(highlightLayer);
                } catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }

                this.repaint();
            }
        }
    }

    protected void setZoomOut(java.awt.geom.Point2D pnt)
    {
        double width = env.getWidth() * ZoomOutFactor;
        double height = env.getHeight() * ZoomOutFactor;
        double x = pnt.getX() - (0.5 * width);
        double y = pnt.getY() - (0.5 * height);
        double x2 = pnt.getX() + (0.5 * width);
        double y2 = pnt.getY() + (0.5 * height);
        env = new ReferencedEnvelope(x, x2, y, y2, mapContext
                .getCoordinateReferenceSystem());
        this.repaint();
    }

    public void zoomToExtent()
    {
        try
        {
            env = mapContext.getLayerBounds();
            this.repaint();
        } catch (Exception e)
        {
        }
    }

    public void zoomRectangle(ReferencedEnvelope newArea)
    {
        env = newArea;
        this.repaint();
    }

    int oldX = 0;
    int oldY = 0;

    public void updatePanning(MouseEvent e)
    {
        if ((oldX == 0) && (oldY == 0))
        {
            oldX = e.getX();
            oldY = e.getY();
        }
        int dx = oldX - e.getX();
        int dy = e.getY() - oldY;
        System.out.println("" + oldX + " " + oldY);
        oldX = e.getX();
        oldY = e.getY();
        double mdx = (dx * ((env.getMaxX() - env.getMinX()) / this.getWidth()));
        double mdy = (dy * ((env.getMaxY() - env.getMinY()) / this.getHeight()));
        panning(mdx, mdy);

    }

    public void panning(double dx, double dy)
    {
        double newX = env.getMinX() + dx;
        double newY = env.getMinY() + dy;
        double newX2 = env.getMaxX() + dx;
        double newY2 = env.getMaxY() + dy;
        env = new ReferencedEnvelope(newX, newX2, newY, newY2, mapContext
                .getCoordinateReferenceSystem());
        this.repaint();
    }

    public DefaultMapLayer makePolyLayer(ShapefileDataStore ds, Color hue,
            Color color2, String layerName)
    {
        FeatureSource<SimpleFeatureType, SimpleFeature> fs = null;
        try
        {
            fs = ds.getFeatureSource();
        } catch (IOException e)
        {
        }
        return makePolyLayer(fs, hue, color2, layerName);
    }

    public DefaultMapLayer makePolyLayer(
            FeatureSource<SimpleFeatureType, SimpleFeature> fs, Color hue,
            Color color2, String layerName)
    {
        StyleBuilder sb = new StyleBuilder();
        PolygonSymbolizer sym = sb.createPolygonSymbolizer(hue, color2, 0);
        Style sty = sb.createStyle(sym);
        return new DefaultMapLayer(fs, sty, layerName);
    }

    public DefaultMapLayer makeLineLayer(ShapefileDataStore ds, Color hue,
            String layerName)
    {
        DefaultMapLayer aLayer = null;
        try
        {
            FeatureSource<SimpleFeatureType, SimpleFeature> fs = ds
                    .getFeatureSource();
            StyleBuilder sb = new StyleBuilder();
            LineSymbolizer sym = sb.createLineSymbolizer(hue, 2);
            Style sty = sb.createStyle(sym);
            aLayer = new DefaultMapLayer(fs, sty, layerName);
        } catch (MalformedURLException e)
        {
            System.err.println("Bad Shapefile Name or Shapefile not found.");
            e.printStackTrace();
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        return aLayer;
    }

    public DefaultMapLayer makePointsLayer(ShapefileDataStore ds,
            String layerName, Color color1)
    {
        DefaultMapLayer aLayer = null;
        try
        {
            FeatureSource<SimpleFeatureType, SimpleFeature> fs = ds
                    .getFeatureSource();
            StyleBuilder sb = new StyleBuilder();
            Mark circle = sb.createMark(StyleBuilder.MARK_CIRCLE, color1);
            Graphic graph2 = sb.createGraphic(null, circle, null, 1, 6, 0);
            PointSymbolizer sym = sb.createPointSymbolizer(graph2);
            Style sty = sb.createStyle(sym);
            aLayer = new DefaultMapLayer(fs, sty, layerName);
        } catch (MalformedURLException e)
        {
            System.err.println("Bad Shapefile Name or Shapefile not found.");
            e.printStackTrace();
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        return aLayer;
    }

    public static int ZOOMIN = 0;
    public static int ZOOMOUT = 1;
    public static int GETFEATURE = 2;
    public static int PAN = 3;
    public static int GETAREA = 4;
    public static int ZOOMTOEXTENT = 5;
    protected int ToolInEffect = GETFEATURE;

    protected static int ZoomFactor = 2;
    protected static int ZoomOutFactor = 3;

    public int getTool()
    {
        return ToolInEffect;
    }

    public void setTool(int tool)
    {
        ToolInEffect = tool;
    }

    public void addMapLayerWithDialog()
    {
        JFileChooser chooser = new JFileChooser();
        chooser.setDialogTitle("Choose map file for layer");
        FileNameExtensionFilter filter = new FileNameExtensionFilter(
                "Shape files", "shp");
        chooser.setFileFilter(filter);
        int returnVal = chooser.showOpenDialog(this);
        if (returnVal == JFileChooser.APPROVE_OPTION)
        {
            addMapLayer(chooser.getSelectedFile().getAbsolutePath(), "", true,
                    true);
        }
    }

    public MapLayer addMapLayer(String fileName, String layerName,
            boolean useColorDialog, boolean addToMapContext)
    {
        MapLayer result = null;
        try
        {
            File file = new File(fileName);
            URL rdURL = file.toURI().toURL();
            if (!file.exists())
            {
                rdURL = getClass().getResource(fileName);
            }
            ShapefileDataStore ds = new ShapefileDataStore(rdURL);
            if (layerName == "")
            {
                layerName = file.getName();
            }
            result = addMapLayer(ds, layerName, useColorDialog, addToMapContext);
        } catch (MalformedURLException e)
        {
            System.err.println("Bad Shapefile Name or Shapefile not found.");
            e.printStackTrace();
        }
        return result;
    }

    public MapLayer addMapLayer(String fileName, String layerName)
    {
        return addMapLayer(fileName, layerName, false, true);
    }

    public void highlightTidyUp()
    {
        mapContext = getMapContext();
        if (highlightLayer != null)
        {
            if (mapContext.indexOf(highlightLayer) >= 0)
            {
                mapContext.removeLayer(highlightLayer);
                highlightLayer = null;
            }
        }
    }

    public MapLayer addMapLayer(ShapefileDataStore ds, String layerName,
            boolean useColorDialog, boolean addToMapContext)
    {
        MapLayer result = null;
        try
        {
            mapContext = getMapContext();
            highlightTidyUp();
            Class<?> type = ds.getSchema().getGeometryDescriptor().getType()
                    .getBinding();
            if (type.isAssignableFrom(Polygon.class)
                    || type.isAssignableFrom(MultiPolygon.class))
            {
                Color color1 = null;
                Color color2 = null;
                if (useColorDialog)
                {
                    color1 = JColorChooser.showDialog(null,
                            "Choose Polygon lines color", Color.BLACK);
                    color2 = JColorChooser.showDialog(null,
                            "Choose polygon area color", Color.BLUE);
                }
                if (color1 == null)
                {
                    color1 = Color.BLACK;
                }
                if (color2 == null)
                {
                    color2 = Color.BLUE;
                }
                result = makePolyLayer(ds, color2, color1, layerName);
            } else if (type.isAssignableFrom(LineString.class)
                    || type.isAssignableFrom(MultiLineString.class))
            {
                Color color1 = null;
                if (useColorDialog)
                {
                    color1 = JColorChooser.showDialog(null,
                            "Choose lines color", Color.BLACK);
                }
                if (color1 == null)
                {
                    color1 = Color.BLACK;
                }
                result = makeLineLayer(ds, color1, layerName);

            } else
            {
                Color color1 = null;
                if (useColorDialog)
                {
                    color1 = JColorChooser.showDialog(null,
                            "Choose points color", Color.BLACK);
                }
                if (color1 == null)
                {
                    color1 = Color.YELLOW;
                }
                result = makePointsLayer(ds, layerName, color1);

            }
            if (addToMapContext)
            {
                mapContext.addLayer(result);
                if (renderer == null)
                {
                    renderer = new ShapefileRenderer(mapContext);
                }
                this.repaint();
                if (layersPanel != null)
                {
                    layersPanel.addLayer();
                }
            }
        } catch (Exception e)
        {
            e.printStackTrace();
        }
        return result;
    }

    public void hideLayer(int layerNo)
    {
        try
        {
            mapContext.getLayer(layerNo).setVisible(false);
            if (layerNo == activeLayer)
            {
                setActiveLayer(lastAvaibleLayer());
            }
            this.repaint();
        } catch (Exception e)
        {
        }
    }

    public void showLayer(int layerNo)
    {
        try
        {
            mapContext.getLayer(layerNo).setVisible(true);
            this.repaint();
        } catch (Exception e)
        {
        }
    }

    public int lastAvaibleLayer()
    {
        for (int i = mapContext.getLayerCount() - 1; i >= 0; i--)
        {
            MapLayer layer = mapContext.getLayer(i);
            if (layer.isVisible() && (layer != highlightLayer))
            {
                return i;
            }
        }
        return 0;
    }

    public void removeLayer(int layerNo)
    {
        try
        {
            mapContext.removeLayer(layerNo);
            if (layerNo == activeLayer)
            {
                setActiveLayer(lastAvaibleLayer());
            }
            this.repaint();
        } catch (Exception e)
        {
        }
    }

    public int findLayerNo(String layerName)
    {
        for (int i = 0; i < mapContext.getLayerCount(); i++)
        {
            if (mapContext.getLayer(i).getTitle().equals(layerName))
            {
                return i;
            }
        }
        return -1;
    }

    public void showLayer(String layerName)
    {
        showLayer(findLayerNo(layerName));
    }

    public void hideLayer(String layerName)
    {
        hideLayer(findLayerNo(layerName));
    }

    public void removeLayer(String layerName)
    {
        removeLayer(findLayerNo(layerName));
    }

    public MapContext getMapContext()
    {
        if (mapContext == null)
        {
            CoordinateReferenceSystem crs = null;
            try
            {
                crs = CRS.decode("EPSG:2600");
            } catch (NoSuchAuthorityCodeException e)
            {
                e.printStackTrace();
            } catch (FactoryException e)
            {
                e.printStackTrace();
            }
            mapContext = new DefaultMapContext(crs);
        }

        return mapContext;
    }

    ReferencedEnvelope env = null;

    public void updateRect(Graphics g)
    {
        if (currentRect != null)
        {
            g.setXORMode(Color.white);
            g.drawRect(rectToDraw.x, rectToDraw.y, rectToDraw.width - 1,
                    rectToDraw.height - 1);
        }

    }

    int activeLayer = 0;

    public void setActiveLayer(int actLayer)
    {
        if (mapContext != null)
        {
            if (mapContext.getLayer(actLayer).isVisible())
            {
                activeLayer = actLayer;
            }
        }
        if (highlightLayer != null)
        {
            if (mapContext.indexOf(highlightLayer) >= 0)
            {
                mapContext.removeLayer(highlightLayer);
                highlightLayer = null;
            }
        }
    }

    public int getActiveLayer()
    {
        return activeLayer;
    }

    double ratio = 0;

    private void initStartLayers()
    {
        if ((mapContext.getLayerCount() == 0) && (layersPanel != null))
        {
            this.addMapLayer("/lt/vu/mif/gt/mapviewer/data/maps/apskrity.shp",
                    "", false, true);
        }
    }

    private int findLayer(String layerFName)
    {
        MapLayer[] layers = mapContext.getLayers();
        try
        {
            for (int i = 0; i < layers.length; i++)
            {
                if (layers[i] != null)
                {
                    if (layers[i].getTitle().equals(layerFName))
                    {
                        return i;
                    }
                }
            }
        } catch (Exception ee)
        {
        }
        return -1;
    }

    private void removeDefLayer(String layerFname)
    {
        int index = findLayer(layerFname);
        if (index >= 0)
        {
            // this.removeLayer(index);
            layersPanel.removeLayer(index);
            // layersPanel.repaint();
        }
    }

    private MapLayer addDefLayer(String layerFName)
    {
        int index = findLayer(layerFName);
        if (index < 0)
        {
            return this.addMapLayer("/lt/vu/mif/gt/mapviewer/data/maps/"
                    + layerFName, "", false, false);
            // return mapContext.getLayer(mapContext.getLayerCount() - 1);
        } else
        {
            return mapContext.getLayer(index);
        }

    }

    public int MaxWidth = 0;
    public int MaxHeight = 0;

    @Override
    public void paint(Graphics g)
    {

        super.paint(g);

        try
        {
            int maxY = this.getHeight();
            int maxX = this.getWidth();
            getMapContext();
            initStartLayers();
            if (env == null)
            {
                if (mapContext != null)
                {
                    env = mapContext.getLayerBounds();
                    if (env != null)
                    {
                        double hei = env.getMaxY() - env.getMinY();
                        if (hei == 0)
                        {
                            hei = 1;
                        }
                        double wi = env.getMaxX() - env.getMinX();
                        if (wi == 0)
                        {
                            wi = 1;
                        }
                        ratio = (wi) / (hei);
                    }
                }
            } else
            {
                double mineX = env.getMinX();
                double maxeX = env.getMaxX();
                double mineY = env.getMinY();
                double maxeY = env.getMaxY();
                double wid = maxeX - mineX;
                double hei = maxeY - mineY;
                double hei2 = hei;
                double wid2 = wid;
                if (wid > hei)
                {
                    hei2 = wid / ratio;
                } else
                {
                    wid2 = hei * ratio;
                }
                if (hei2 < hei)
                {
                    hei2 = hei;
                    wid2 = hei * ratio;
                }
                maxeX = mineX + wid2;
                maxeY = mineY + hei2;

                env = new ReferencedEnvelope(mineX, maxeX, mineY, maxeY,
                        mapContext.getCoordinateReferenceSystem());
            }
            image = new BufferedImage(maxX, maxY, BufferedImage.TYPE_INT_RGB);
            int newY = (int) (maxX / ratio);
            int newX = maxX;
            if (newY > maxY)
            {
                newY = maxY;
                newX = (int) (newY * ratio);
            }
            Graphics2D ig = image.createGraphics();
            MaxWidth = newX;
            MaxHeight = newY;
            Rectangle r = new Rectangle(0, 0, maxX, maxY);
            renderer = new ShapefileRenderer(mapContext);
            renderer.paint(ig, r, env);
            g.drawImage(image, 0, 0, newX, newY, this);
            updateRect(g);
        } catch (Exception e)
        {
            e.printStackTrace();
        }

    }

}
