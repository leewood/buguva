package lt.vu.mif.gt.mapviewer;

import java.awt.Color;
import java.awt.Component;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;

import org.geotools.map.MapContext;
import org.geotools.map.MapLayer;
import org.geotools.styling.PolygonSymbolizer;
import org.opengis.geometry.coordinate.LineString;
import org.opengis.geometry.coordinate.Polygon;

import com.vividsolutions.jts.geom.MultiLineString;
import com.vividsolutions.jts.geom.MultiPolygon;

public class LayersPanel extends JPanel
{

    private static final long serialVersionUID = 4351290968229177021L;
    MapPanel map = null;

    private void clickRemoveButton(int layerNo, MouseEvent event)
    {
        layerNo = this.getLayerNo((JButton) event.getSource());
        removeLayer(layerNo);
    }

    private void clickUpButton(int layerNo, MouseEvent event)
    {
        layerNo = this.getLayerNo((JButton) event.getSource());
        moveUp(layerNo);
    }

    private void clickDownButton(int layerNo, MouseEvent event)
    {
        layerNo = this.getLayerNo((JButton) event.getSource());
        moveDown(layerNo);
    }

    public LayersPanel()
    {
        GridLayout layout = new GridLayout(100, 1);
        this.setAlignmentX(LEFT_ALIGNMENT);
        this.setLayout(layout);
    }

    public void removeLayer(int layerNo)
    {
        if (map != null)
        {
            map.removeLayer(layerNo);
            this.remove(layerNo + 1);
            updateActivityButtons();
            this.repaint();
        }
    }

    public void setActivity(JButton button, boolean isActive)
    {
        if (isActive)
        {
            button.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/page_edit.png")));
            button.setToolTipText("make layer active");
        } else
        {
            button.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/clear.png")));
            button.setToolTipText("make layer active");
        }

    }

    public void moveUp(int layerNo)
    {
        if (layerNo > 0)
        {
            map.getMapContext().moveLayer(layerNo, layerNo - 1);
            Component c1 = this.getComponent(layerNo + 1);
            Component c2 = this.getComponent(layerNo);
            this.remove(c1);
            this.remove(c2);
            this.add(c1, layerNo);
            this.add(c2, layerNo + 1);
            this.repaint();
            map.repaint();
        }
    }

    public void moveDown(int layerNo)
    {
        if (layerNo < map.getMapContext().getLayerCount() - 1)
        {
            map.getMapContext().moveLayer(layerNo, layerNo + 1);
            Component c1 = this.getComponent(layerNo + 1);
            Component c2 = this.getComponent(layerNo + 2);
            this.remove(c1);
            this.remove(c2);
            this.add(c2, layerNo + 1);
            this.add(c1, layerNo + 2);
            this.repaint();
            map.repaint();
        }
    }

    public void addLayer()
    {
        if (map != null)
        {
            int i = map.getMapContext().getLayerCount() - 1;
            MapLayer layer = map.getMapContext().getLayer(i);
            JPanel jPanel0 = new JPanel();
            jPanel0.add("up" + i, createUpButton(i, layer.getTitle()));
            jPanel0.add("down" + i, createDownButton(i, layer.getTitle()));
            jPanel0.add("rem" + i, createRemoveButton(i, layer.getTitle()));
            jPanel0.add("sho" + i, createShowHideButton(i, layer.getTitle(),
                    layer.isVisible()));
            // layer.getStyle()
            int type = 0;

            Color color1 = null;
            Color color2 = null;
            Class<?> cl = layer.getFeatureSource().getSchema()
                    .getGeometryDescriptor().getType().getBinding();
            if (cl.isAssignableFrom(Polygon.class)
                    || cl.isAssignableFrom(MultiPolygon.class))
            {
                type = 0;
                org.geotools.styling.Style style = layer.getStyle();
                PolygonSymbolizer sym = (PolygonSymbolizer) (style
                        .getFeatureTypeStyles()[0].getRules()[0]
                        .getSymbolizers()[0]);
                color1 = Color.decode("0x"
                        + sym.getStroke().getColor().toString().substring(1));
                System.out.println("0x"
                        + sym.getFill().getColor().toString().substring(1));
                color2 = Color.decode("0x"
                        + sym.getFill().getColor().toString().substring(1));
                System.out.println(color2);

            } else if (cl.isAssignableFrom(LineString.class)
                    || cl.isAssignableFrom(MultiLineString.class))
            {
                type = 1;
                org.geotools.styling.Style style = layer.getStyle();
                org.geotools.styling.LineSymbolizerImpl sym = (org.geotools.styling.LineSymbolizerImpl) (style
                        .getFeatureTypeStyles()[0].getRules()[0]
                        .getSymbolizers()[0]);
                color1 = Color.decode("0x"
                        + sym.getStroke().getColor().toString().substring(1));
            } else
            {
                type = 2;
            }
            jPanel0.add("typ" + i, this.createTypeLabel(type, color1, color2));
            jPanel0.add("act" + i, this.createActivityButton(i, i == map
                    .getActiveLayer()));
            jPanel0.setAlignmentX(LEFT_ALIGNMENT);
            this.add(jPanel0);
            updateActivityButtons();
            this.doLayout();
            this.repaint();
        }
    }

    public void updateButton(int i)
    {
        if (map != null)
        {
            MapLayer layer = map.getMapContext().getLayer(i);
            String layerName = layer.getTitle();
            boolean visible = layer.isVisible();

            JButton jButton0 = (JButton) (((JPanel) (this.getComponent(i + 1)))
                    .getComponent(3));
            setShowHideButton(layerName, visible, jButton0);
        }
    }

    private void updateActivityButtons()
    {
        int active = map.getActiveLayer();
        for (int i = 1; i < this.getComponentCount(); i++)
        {
            JButton jButton0 = (JButton) (((JPanel) (this.getComponent(i)))
                    .getComponent(5));
            setActivity(jButton0, i - 1 == active);
        }
    }

    private void clickSetActive(MouseEvent event)
    {
        int layerNo = this.getLayerNo((JButton) event.getSource());
        map.setActiveLayer(layerNo);
        updateActivityButtons();
        repaint();
    }

    private void clickSetVisibleButton(int layerNo, MouseEvent event,
            boolean visible)
    {
        layerNo = this.getLayerNo((JButton) event.getSource());

        visible = !getVisib(layerNo);
        if (map != null)
        {
            if (visible)
            {
                map.showLayer(layerNo);
            } else
            {
                map.hideLayer(layerNo);
            }
            this.updateButton(layerNo);
            updateActivityButtons();
            repaint();
        }
    }

    public void setMapPanel(MapPanel panel)
    {
        map = panel;
    }

    private JButton createRemoveButton(final int layerNo, String layerName)
    {
        JButton jButton0;
        jButton0 = new JButton();
        jButton0.setName("rem" + layerNo);
        jButton0.setIcon(new ImageIcon(getClass().getResource(
                "/lt/vu/mif/gt/mapviewer/data/pics/cross.png")));
        jButton0.setToolTipText("remove layer '" + layerName + "'");
        jButton0.setBorder(null);
        jButton0.setDefaultCapable(false);
        jButton0.addMouseListener(new MouseAdapter()
        {
            @Override
            public void mousePressed(MouseEvent event)
            {
                clickRemoveButton(layerNo, event);
            }
        });
        return jButton0;
    }

    private JButton createUpButton(final int layerNo, String layerName)
    {
        JButton jButton0;
        jButton0 = new JButton();
        jButton0.setName("up" + layerNo);
        jButton0.setIcon(new ImageIcon(getClass().getResource(
                "/lt/vu/mif/gt/mapviewer/data/pics/arrow_up.png")));
        jButton0.setToolTipText("move layer '" + layerName + "' up");
        jButton0.setBorder(null);
        jButton0.setDefaultCapable(false);
        jButton0.addMouseListener(new MouseAdapter()
        {
            @Override
            public void mousePressed(MouseEvent event)
            {
                clickUpButton(layerNo, event);
            }
        });
        return jButton0;
    }

    private JButton createDownButton(final int layerNo, String layerName)
    {
        JButton jButton0;
        jButton0 = new JButton();
        jButton0.setName("down" + layerNo);
        jButton0.setIcon(new ImageIcon(getClass().getResource(
                "/lt/vu/mif/gt/mapviewer/data/pics/arrow_down.png")));
        jButton0.setToolTipText("move layer '" + layerName + "' down");
        jButton0.setBorder(null);
        jButton0.setDefaultCapable(false);
        jButton0.addMouseListener(new MouseAdapter()
        {
            @Override
            public void mousePressed(MouseEvent event)
            {
                clickDownButton(layerNo, event);
            }
        });
        return jButton0;
    }

    public void setShowHideButton(String layerName, boolean visible,
            JButton jButton0)
    {
        jButton0.setText(layerName);
        if (visible)
        {
            jButton0.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/lightbulb.png")));
            jButton0.setToolTipText("hide layer '" + layerName + "'");
        } else
        {
            jButton0.setIcon(new ImageIcon(getClass().getResource(
                    "/lt/vu/mif/gt/mapviewer/data/pics/lightbulb_off.png")));
            jButton0.setToolTipText("show layer '" + layerName + "'");
        }

    }

    public int getLayerNo(JButton button)
    {
        for (int i = 1; i < this.getComponentCount(); i++)
        {
            JPanel panel = (JPanel) (this.getComponent(i));
            for (int j = 0; j < panel.getComponentCount(); j++)
            {
                if (panel.getComponent(j).equals(button))
                {
                    return i - 1;
                }
            }
        }
        return 0;
    }

    public boolean getVisib(int layerNo)
    {
        if (map != null)
        {
            return map.getMapContext().getLayer(layerNo).isVisible();
        }
        return false;
    }

    public JPanel createTypeLabel(int type, Color color1, Color color2)
    {
        JPanel result = new JPanel();

        if (type == 0)
        {
            result.setBackground(color2);
            result.setForeground(color2);
            Border border = new LineBorder(color1);
            result.setBorder(border);
            result.setSize(30, 30);
            // result.setText("   ");
        } else if (type == 1)
        {
            // result.setText("---");
            // result.setForeground(color1);
            result.setBorder(null);
            JLabel label = new JLabel();
            label.setForeground(color1);
            label.setText("---");
            result.add(label);
        } else
        {
            // result.setText(".");
            JLabel label = new JLabel();
            label.setText(".");
            result.add(label);
        }
        return result;
    }

    private JButton createShowHideButton(final int layerNo, String layerName,
            final boolean visible)
    {
        JButton jButton0;
        jButton0 = new JButton();
        setShowHideButton(layerName, visible, jButton0);
        jButton0.setBorder(null);
        jButton0.setDefaultCapable(false);
        jButton0.addMouseListener(new MouseAdapter()
        {
            @Override
            public void mousePressed(MouseEvent event)
            {
                clickSetVisibleButton(layerNo, event, !visible);
            }
        });

        jButton0.setSize(150, 30);
        return jButton0;
    }

    private JButton createActivityButton(final int layerNo, boolean isActive)
    {
        JButton jButton0;
        jButton0 = new JButton();
        this.setActivity(jButton0, isActive);
        jButton0.setBorder(null);
        jButton0.setDefaultCapable(false);
        jButton0.addMouseListener(new MouseAdapter()
        {
            @Override
            public void mousePressed(MouseEvent event)
            {

                clickSetActive(event);
            }
        });
        return jButton0;
    }

    public JLabel createLayerLabel(int layerNo, String layerName)
    {
        JLabel jLabel0 = new JLabel();
        jLabel0.setText(layerName);
        jLabel0.setName("lab" + layerNo);
        jLabel0.setBackground(Color.BLACK);
        jLabel0.setForeground(Color.WHITE);
        /*
         * jLabel0.addMouseListener(new MouseAdapter() {
         * 
         * @Override public void mousePressed(MouseEvent event) {
         * jLabel0MouseMousePressed(event); } });
         */
        return jLabel0;
    }

    public void updateLayersFromMapContext(MapContext context)
    {
        for (int i = 0; i < context.getLayerCount(); i++)
        {
            MapLayer layer = context.getLayer(i);
            JPanel jPanel0 = new JPanel();
            if (this.getComponentCount() > i)
            {
                jPanel0 = (JPanel) this.getComponent(i);
                jPanel0.removeAll();
            }
            jPanel0.add("up" + i, createUpButton(i, layer.getTitle()));
            jPanel0.add("down" + i, createDownButton(i, layer.getTitle()));
            jPanel0.add("rem" + i, createRemoveButton(i, layer.getTitle()));
            jPanel0.add("sho" + i, createShowHideButton(i, layer.getTitle(),
                    layer.isVisible()));
            if (this.getComponentCount() > i)
            {
            } else
            {
                this.add(jPanel0);
            }
        }
    }

    public void updateLayers()
    {
        if (map != null)
        {
            MapContext context = map.getMapContext();
            int count = context.getLayerCount();
            if (count < 100)
            {
                count = 100;
            }
            GridLayout layout = new GridLayout(count, 1);
            // updateLayersFromMapContext(context);
            this.setLayout(layout);
        }
        this.doLayout();
    }

    @Override
    public void paint(Graphics g)
    {
        this.doLayout();
        super.paint(g);
        for (int i = 1; i < this.getComponentCount(); i++)
        {

            this.getComponent(0).repaint();
        }
    }

}
