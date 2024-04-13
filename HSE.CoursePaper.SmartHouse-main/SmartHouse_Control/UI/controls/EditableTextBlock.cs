using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace SmartHouse_Control.UI.controls
{
    /// <summary>
    ///     Class for control
    /// </summary>
    public class EditableTextBlock : TextBlock
    {
        #region Usual Methods

        /// <summary>
        ///     Is editable mode
        /// </summary>
        public bool IsInEditMode {
            get => (bool) GetValue(IsInEditModeProperty);
            set {
                var prevVal = (bool) GetValue(IsInEditModeProperty);
                SetValue(IsInEditModeProperty, value);
                if (prevVal && !value && _valueSet != null) _valueSet.Invoke();
            }
        }

        #endregion

        #region Get functions

        /// <summary>
        ///     Gets or sets the length of the max
        /// </summary>
        /// <value>The length of the max.</value>
        public int MaxLength {
            get => (int) GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        #endregion

        #region Update TextBox

        /// <summary>
        ///     Use another info for the text source
        /// </summary>
        /// <param name="obj">The obj</param>
        private static void IsInEditModeUpdate(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var textBlock = obj as EditableTextBlock;
            if (null != textBlock) {
                //Get the adorner layer of the uielement (here TextBlock)
                var layer = AdornerLayer.GetAdornerLayer(textBlock);

                //If the IsInEditMode set to true means the user has enabled the edit mode
                if (textBlock.IsInEditMode) {
                    if (null == textBlock._adorner) {
                        textBlock._adorner = new EditableTextBlockAdorner(textBlock);

                        //Events wired to exit edit mode when the user presses Enter key or leaves the control
                        textBlock._adorner.TextBoxKeyUp += textBlock.TextBoxKeyUp;
                        textBlock._adorner.TextBoxLostFocus += textBlock.TextBoxLostFocus;
                    }

                    layer.Add(textBlock._adorner);
                }
                else {
                    //Remove the adorner from the adorner layer
                    var adorners = layer.GetAdorners(textBlock);
                    if (adorners != null)
                        foreach (var adorner in adorners)
                            if (adorner is EditableTextBlockAdorner)
                                layer.Remove(adorner);

                    //Update the textblock's text binding
                    var expression = textBlock.GetBindingExpression(TextProperty);
                    if (null != expression) expression.UpdateTarget();
                }
            }
        }

        #endregion

        #region Delegates

        /// <summary>
        ///     Delegate for change box
        /// </summary>
        public delegate void onValueSet();

        //Event
        private onValueSet _valueSet;

        public event onValueSet ValueSet {
            add => _valueSet = value;
            remove => _valueSet = null;
        }

        #endregion

        #region Fields

        private EditableTextBlockAdorner _adorner;

        // Settings InEditMode
        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(EditableTextBlock),
                new UIPropertyMetadata(false, IsInEditModeUpdate));

        // Settings MaxLength
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(EditableTextBlock), new UIPropertyMetadata(0));

        #endregion

        #region Mouse events

        /// <summary>
        ///     release the edit mode when user presses enter
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void TextBoxKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) IsInEditMode = false;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e) {
            IsInEditMode = false;
        }

        /// <summary>
        ///     Invoked when an unhandled
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e) {
            if (e.MiddleButton == MouseButtonState.Pressed)
                IsInEditMode = true;
            else if (e.ClickCount == 2) IsInEditMode = true;
        }

        #endregion
    }
}