package lt.vu.mif.gt.mapviewer.utils;

/**
 * Listener interface to receive events from {@link ModalProgressDialog}
 * 
 * @author King Lung Chiu: kinglung.chiu@gmail-nospam-.com
 */
public interface ProgressDialogListener
{
    /**
     * Called when the user chooses to close the dialog.
     * <p>
     * 
     * What represents 'close' is arbitrary. eg. the event sender (the dialog)
     * could implement 'close' by invoking this method on its listeners when the
     * user clicks the close button on the top right hand corner of the dialog.
     */
    public void onClosingDialog();

    /**
     * Called when the user chooses to cancel the operation represented by the
     * progress dialog.
     * <p>
     * 
     * What represents 'cancel' is arbitrary. eg. the event sender (the dialog)
     * could implement 'cancel' by invoking this method on its listeners when
     * the user clicks the close button on the top right hand corner of the
     * dialog. (yes, same as the previous example, except it's now interpreted
     * as 'cancel' instead of 'close', where 'close' may not actually cancel the
     * operation)
     */
    public void onCancellingOperation();
}
