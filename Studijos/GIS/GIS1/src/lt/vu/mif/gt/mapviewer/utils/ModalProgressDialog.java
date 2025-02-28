package lt.vu.mif.gt.mapviewer.utils;

import java.awt.FlowLayout;
import java.awt.Frame;
import java.lang.reflect.InvocationTargetException;

import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JProgressBar;
import javax.swing.SwingUtilities;

/**
 * A modal progress dialog, with convenience methods for setting dialog
 * properties asynchronously from threads.
 * <p>
 * 
 * This dialog has these changeable properties:
 * <ul>
 * - dialog title
 * </ul>
 * <ul>
 * - dialog message
 * </ul>
 * <ul>
 * - dialog note
 * </ul>
 * <ul>
 * - progress bar minimum, maximum and current values
 * </ul>
 * <ul>
 * - whether to display progress bar's current progress percentage
 * </ul>
 * <ul>
 * - whether the progress bar is indeterminant
 * </ul>
 * 
 * This dialog also generates {@link ProgressDialogListener#onClosingDialog()}
 * events for a registered {@link ProgressDialogListener} when the close button
 * on its top right-hand corner is clicked.
 * 
 * @author King Lung Chiu: kinglung.chiu@gmail-nospam-.com
 */
public class ModalProgressDialog
{

    private JDialog dialog;
    private Frame parent;
    private JLabel messageLabel;
    private JLabel noteLabel;
    private JProgressBar progressBar;
    private ProgressDialogListener dialogListener = null;

    // private boolean visible = false;

    /**
     * Create a new ModalProgressDialog, with an indeterminant progress bar, and
     * with the percentage of current progress displayed.
     * <p>
     * 
     * This is the same as using ModalProgressDialog(parent, title, message,
     * note, minimum, maximum, true)
     * 
     * @see #ModalProgressDialog(Frame parent, String title, String message,
     *      String note, int minimum, int maximum, boolean paintString)
     */
    public ModalProgressDialog(Frame parent, String title, String message,
            String note, int minimum, int maximum)
    {
        this(parent, title, message, note, minimum, maximum, true);
    }

    /**
     * Create a new ModalProgressDialog, with an indeterminant progress bar.
     * 
     * @param parent
     *            The lock target when this dialog is set visible.
     * 
     * @param title
     *            Ttle of this dialog.
     * 
     * @param message
     *            Message displayed in this dialog.
     * 
     * @param note
     *            Note displayed in this dialog.
     * 
     * @param minimum
     *            The minimum value to be shown on the progress bar.
     * 
     * @param maximum
     *            The maximum value to be shown on the progress bar.
     * 
     * @param paintString
     *            Specifies whether to display the % of progress on the progress
     *            bar. Set to true if it should be displayed, false if not.
     */
    public ModalProgressDialog(Frame parent, String title, String message,
            String note, int minimum, int maximum, boolean paintString)
    {
        this.parent = parent;

        initComponents(title, message, note, minimum, maximum);
        dialog.setLocationRelativeTo(parent);
        setStringPainted(paintString);
    }

    /**
     * Add a ProgressDialogListener to this dialog.
     * <p>
     * 
     * This dialog only holds 1 listener at a time, so each call of this method
     * will replace the existing listener with the new one.
     * 
     * @param listener
     *            The listenr to add.
     */
    public void addProgressDialogListener(ProgressDialogListener listener)
    {
        dialogListener = listener;
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    // <editor-fold defaultstate="collapsed" desc=" Generated Code ">
    private void initComponents(String title, String message, String note,
            int min, int max)
    {
        messageLabel = new javax.swing.JLabel();
        setMessage(message);

        noteLabel = new javax.swing.JLabel();
        setNote(note);

        progressBar = new javax.swing.JProgressBar();
        progressBar.setIndeterminate(true);
        progressBar.setMinimum(min);
        progressBar.setMaximum(max);
        progressBar.setStringPainted(true);

        dialog = new JDialog(parent, true);
        dialog.setTitle(title);
        dialog.setLocationRelativeTo(parent);

        dialog
                .setDefaultCloseOperation(javax.swing.WindowConstants.DO_NOTHING_ON_CLOSE);
        dialog.addWindowListener(new java.awt.event.WindowAdapter()
        {
            public void windowClosing(java.awt.event.WindowEvent evt)
            {
                formWindowClosing(evt);
            }
        });

        java.awt.FlowLayout layout = new FlowLayout();
        dialog.getContentPane().setLayout(layout);
        dialog.add(messageLabel);
        dialog.add(noteLabel);
        dialog.add(progressBar);
        /*
         * 
         * layout.setHorizontalGroup(
         * layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
         * .add(layout.createSequentialGroup() .addContainerGap()
         * .add(layout.createParallelGroup
         * (org.jdesktop.layout.GroupLayout.LEADING) .add(messageLabel)
         * .add(noteLabel) // .add(progressBar,
         * org.jdesktop.layout.GroupLayout.PREFERRED_SIZE,
         * org.jdesktop.layout.GroupLayout.DEFAULT_SIZE,
         * org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)) //
         * .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE,
         * Short.MAX_VALUE)) .add(progressBar,
         * org.jdesktop.layout.GroupLayout.DEFAULT_SIZE,
         * org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
         * .addContainerGap()) ); layout.setVerticalGroup(
         * layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
         * .add(layout.createSequentialGroup() .addContainerGap()
         * .add(messageLabel)
         * .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
         * .add(noteLabel)
         * .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
         * .add(progressBar, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE,
         * org.jdesktop.layout.GroupLayout.DEFAULT_SIZE,
         * org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
         * .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE,
         * Short.MAX_VALUE)) );
         */
        dialog.pack();
    }// </editor-fold>

    private void formWindowClosing(java.awt.event.WindowEvent evt)
    {
        if (dialogListener != null)
            dialogListener.onClosingDialog();
        else
            JOptionPane.showMessageDialog(dialog,
                    "Please wait until the operation completes normally",
                    "Operation disallowed.", JOptionPane.ERROR_MESSAGE);
    }

    /**
     * Opens this progress dialog <em>pseudo</em> asynchronously.
     * <p>
     * 
     * This is different to setVisible(true) in that this method will return
     * after the progress dialog is visible. This method does NOT block until
     * another thread closes the dialog.
     * <p>
     * 
     * This method is designed to be called from outside the AWT event dispatch
     * thread. If it's invoked from the AWT event dispatch thread, the method
     * will return without showing the progress dialog to avoid a deadlock.
     * <p>
     * 
     * NOTE: Incorrect use of this method can deadlock your applicaiton!
     * <p>
     * 
     * This method opens the dialog by placing a Runnable on the AWT event
     * dispatch thread via SwingUtilities.invokeLater(). The method then waits
     * until the dialog is visible, before returning to the caller. Thus this
     * method will only return AFTER the current operation on the AWT event
     * dispatch thread is complete.
     * <p>
     * 
     * This means a deadlock will occur if the caller (or the caller's caller,
     * etc) needs this method to return before the current operation on the
     * event dispatch thread can finish.
     * <p>
     * 
     * Therefore, the recommended way to use this method is as follows:
     * 
     * <pre>
     *    // on the event dispatch thread
     *    Thread longOperation = new Thread(new Runnable() {
     *       public void run()
     *       {
     *          ModalProgressDialog dialog = ...;
     *          dialog.open();
     *          ... whatever operation is required
     *          dialog.close();
     *       }
     *    });
     *    longOperation.start();
     * 
     *    // ... other code on the event dispatch thread that does NOT need
     *    // the thread to finish first to operate correctly: these code must
     *    // finish first before the dialog will be visible.
     * </pre>
     */
    public void open()
    {
        if (SwingUtilities.isEventDispatchThread())
            return;

        final JDialog progressDialog = dialog;
        SwingUtilities.invokeLater(new Runnable()
        {
            public void run()
            {
                progressDialog.setVisible(true);
            }
        });

        while (dialog.isVisible() == false)
            try
            {
                Thread.sleep(100);
            } catch (InterruptedException e)
            {
                // ignore
            }
    }

    /**
     * Closes this ProgressDialog, supplied as a complimentary method to open().
     * Same effect as <code>setVisible(false)</code>.
     */
    public void close()
    {
        this.setVisible(false);
    }

    /**
     * Opens and closes this dialog, depending on <code>wantVisible</code>.
     * 
     * @param wantVisible
     *            If true, opens (displays) this dialog. The method will then
     *            block until the dialog is closed again by another thread.
     *            <p>
     * 
     *            If false, closes this dialog. The method returns after the
     *            dialog is closed.
     */
    public void setVisible(final boolean wantVisible)
    {
        dialog.setVisible(wantVisible);
    }

    /**
     * Tells you whether this dialog is currently displayed (visible).
     * 
     * @return True if it is displayed, false if not.
     */
    public boolean isVisible()
    {
        return dialog.isVisible();
    }

    /**
     * Updates the title, message, note and progress components of this dialog
     * on the AWT event dispatch thread via
     * <code>SwingUtilities.invokeAndWait()</code>.
     * <p>
     * 
     * This method must ONLY be called from a user thread with NOTHING on the
     * AWT event dispatch thread waiting (directly or indirectly) for this call
     * to finish.
     * 
     * @param title
     *            The dialog title to display. If null, the current title is
     *            unchanged.
     * @param message
     *            The message to display.
     * @param note
     *            The note to display.
     * @param progress
     *            The progress value to update the progress bar with.
     */
    public void setDisplayPropertiesNow(final String title,
            final String message, final String note, final int progress)
    {
        try
        {
            SwingUtilities.invokeAndWait(new Runnable()
            {
                public void run()
                {
                    ModalProgressDialog.this.setDisplayProperties(title,
                            message, note, progress);
                }
            });
        } catch (InterruptedException ex)
        {
            ex.printStackTrace();
        } catch (InvocationTargetException ex)
        {
            ex.printStackTrace();
        }
    }

    /**
     * Convenience method for setDisplayPropertiesNow(null, message, note,
     * progress).
     * <p>
     * 
     * Use this if you want to update all the displayed properties except dialog
     * title.
     */
    public void setDisplayPropertiesNow(final String message,
            final String note, final int progress)
    {
        this.setDisplayPropertiesNow(null, message, note, progress);
    }

    /**
     * Updates the title, message, note and progress components of this dialog
     * on the AWT event dispatch thread via
     * <code>SwingUtilities.invokeLater()</code>.
     * 
     * @param title
     *            The dialog title to display. If null, the current title is
     *            unchanged.
     * @param message
     *            The message to display.
     * @param note
     *            The note to display.
     * @param progress
     *            The progress value to update the progress bar with.
     */
    public void setDisplayPropertiesLater(final String title,
            final String message, final String note, final int progress)
    {
        SwingUtilities.invokeLater(new Runnable()
        {
            public void run()
            {
                ModalProgressDialog.this.setDisplayProperties(title, message,
                        note, progress);
            }
        });
    }

    /**
     * Convenience method for setDisplayPropertiesLater(null, message, note,
     * progress).
     * <p>
     * 
     * Use this if you want to update all the displayed properties except dialog
     * title.
     */
    public void setDisplayPropertiesLater(final String message,
            final String note, final int progress)
    {
        this.setDisplayPropertiesLater(null, message, note, progress);
    }

    /**
     * Updates the title, message, note and progress components of this dialog.
     * 
     * @param title
     *            The dialog title to display. If null, the current title is
     *            unchanged.
     * @param message
     *            The message to display.
     * @param note
     *            The note to display.
     * @param progress
     *            The progress value to update the progress bar with.
     */
    public void setDisplayProperties(String title, String message, String note,
            int progress)
    {
        if (title != null)
            setTitle(title);

        setMessage(message);
        setNote(note);
        setProgress(progress);
        dialog.pack();
    }

    /**
     * Convenience method for setDisplayProperties(null, message, note,
     * progress).
     * <p>
     * 
     * Use this if you want to update all the displayed properties except dialog
     * title.
     */
    public void setDisplayProperties(String message, String note, int progress)
    {
        this.setDisplayProperties(null, message, note, progress);
    }

    /**
     * Set the dialog title.
     * 
     * @param title
     *            The new dialog title.
     */
    public void setTitle(String title)
    {
        dialog.setTitle(title);
    }

    /**
     * Set the dialog message.
     * 
     * @param message
     *            The new dialog message.
     */
    public void setMessage(String message)
    {
        messageLabel.setText(message);
    }

    /**
     * Set the dialog note.
     * 
     * @param note
     *            The new dialog note.
     */
    public void setNote(String note)
    {
        noteLabel.setText(note);
    }

    /**
     * Set the progress bar value.
     * 
     * @param progress
     *            The new progress bar value.
     */
    public void setProgress(int progress)
    {
        progressBar.setValue(progress);
        if (progress == progressBar.getMaximum())
            progressBar.setIndeterminate(false);
    }

    /**
     * Specifies whether the % string is displayed on the progress bar.
     * 
     * @param painted
     *            True if the string is to be displayed, false otherwise.
     */
    public void setStringPainted(boolean painted)
    {
        progressBar.setStringPainted(painted);
    }

    /**
     * Specifies whether the progress bar is indeterminant.
     * <p>
     * 
     * Determinant progress bars will progressively increase in value until they
     * reach 100%. Indeterminant progress bars will show progress which
     * constantly slides between 0% and 100%.
     * <p>
     * 
     * Use indeterminant progress bars if the operation you are tracking doesn't
     * give any indication of progress, or if you want to make the progress bar
     * look more active.
     * 
     * @param choice
     *            True if you want the progress bar to be indeterminant. False
     *            otherwise.
     */
    public void setIndeterminant(boolean choice)
    {
        progressBar.setIndeterminate(choice);
    }
}