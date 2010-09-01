package lt.vu.mif.gt.mapviewer;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import javax.swing.JFrame;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.table.DefaultTableModel;

import org.dyno.visual.swing.layouts.Bilateral;
import org.dyno.visual.swing.layouts.Constraints;
import org.dyno.visual.swing.layouts.GroupLayout;

//VS4E -- DO NOT REMOVE THIS LINE!
public class AttributesWindow extends JFrame
{

    private static final long serialVersionUID = 1L;
    private JTable jTable0;
    private JScrollPane jScrollPane0;
    private MapPanel map = null;

    public AttributesWindow(MapPanel map)
    {
        this.map = map;
        initComponents();
    }

    private void initComponents()
    {
        setTitle("Attributes");
        setLayout(new GroupLayout());
        add(getJScrollPane0(), new Constraints(new Bilateral(0, 0, 22),
                new Bilateral(0, 0, 26, 403)));
        setSize(741, 240);
    }

    private JScrollPane getJScrollPane0()
    {
        if (jScrollPane0 == null)
        {
            jScrollPane0 = new JScrollPane();
            jScrollPane0.setViewportView(getJTable0());
        }
        return jScrollPane0;
    }

    public JTable getJTable0()
    {
        if (jTable0 == null)
        {
            jTable0 = new JTable();
            jTable0.setModel(new DefaultTableModel(new Object[][] {},
                    new String[] { "Attribute" })
            {
                private static final long serialVersionUID = 1L;
                Class<?>[] types = new Class<?>[] { Object.class, Object.class, };

                public Class<?> getColumnClass(int columnIndex)
                {
                    return types[columnIndex];
                }
            });
            jTable0.addMouseListener(new MouseAdapter()
            {
                @Override
                public void mouseClicked(MouseEvent event)
                {
                    int row = jTable0.rowAtPoint(event.getPoint());
                    int col = jTable0.columnAtPoint(event.getPoint());
                    if ((row >= 0) && (col >= 0))
                    {
                        String name = jTable0.getModel().getColumnName(col);
                        String value = jTable0.getModel().getValueAt(row, col)
                                .toString();
                        map.selectByAttribute(name, value, row);
                    }
                }
            });

        }
        return jTable0;
    }

}
