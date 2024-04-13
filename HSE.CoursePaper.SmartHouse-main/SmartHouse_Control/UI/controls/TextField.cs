using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartHouse_Control.UI.controls
{
    /// <summary>
    ///     Представляет элемент управления, который может быть использован для отображения или редактирования
    ///     неформатированного текста.
    /// </summary>
    public class TextField : TextBox
    {
        #region Dependancy properties fields

        public static readonly DependencyProperty PlaceholderProperty;
        public static readonly DependencyProperty PlaceholderForegroundProperty;
        public static readonly DependencyProperty IsPlaceholderShowingProperty;

        # endregion Dependancy properties

        #region Private fields

        /// <summary>
        ///     Так как <see cref="TextField.Placeholder" /> и <see cref="TextBox.Text" /> могут иметь разные цвета,
        ///     но для представления обоих используется базовое свойство <see cref="Control.Foreground" />, то для хранения
        ///     цвета для <see cref="TextBox.Text" /> во время отображения <see cref="TextField.Placeholder" /> используется данное
        ///     поле.
        /// </summary>
        private Brush originalForegroundBrush;

        /// <summary>
        ///     Перед отображением <see cref="TextField.Placeholder" /> вызывается данные метод, который указывает,
        ///     нужно ли воспринимать текущее значение свойства <see cref="TextBox.Text" /> как недействительное
        ///     и показывать вместо него <see cref="TextField.Placeholder" />.
        /// </summary>
        private readonly Func<string, bool> textValidationCallback;

        #endregion Private fields

        #region Properties

        /// <summary>
        ///     Получение или изменение текста заполнителя.
        /// </summary>
        public string Placeholder {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        ///     Получение или изменение цвета заполнителя.
        /// </summary>
        public Brush PlaceholderForeground {
            get => (Brush) GetValue(PlaceholderForegroundProperty);
            set => SetValue(PlaceholderForegroundProperty, value);
        }

        /// <summary>
        ///     Считывание или указание, должен ли отображаться заполнитель.
        /// </summary>
        public bool IsPlaceholderShowing {
            get => (bool) GetValue(IsPlaceholderShowingProperty);
            set {
                if (value)
                    ShowPlaceholder(false);
                else
                    HidePlaceholder();
            }
        }

        /// <summary>
        ///     Возвращает текст или пустую строку, в зависимости от того, показывается текст или заполнитель.
        /// </summary>
        public string TextWithoutPlaceholder {
            get {
                if (IsPlaceholderShowing) return string.Empty;

                return Text;
            }
        }

        /// <summary>
        ///     Возвращает или устанавливает текст, хранящийся в текстовом поле.
        /// </summary>
        public new string Text {
            get => base.Text;
            set {
                HidePlaceholder();
                base.Text = value;
                ShowPlaceholder(true);
            }
        }

        #endregion Properties

        #region Builders, On-methods

        static TextField() {
            PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(TextField),
                new PropertyMetadata("Placeholder"));
            PlaceholderForegroundProperty = DependencyProperty.Register("PlaceholderForeground", typeof(Brush),
                typeof(TextField), new PropertyMetadata(Brushes.Gray));
            IsPlaceholderShowingProperty = DependencyProperty.Register("IsPlaceholderShowing", typeof(bool),
                typeof(TextField), new PropertyMetadata(true));
        }

        /// <summary>
        ///     Полный конструткор.
        /// </summary>
        /// <param name="textValidationCallback">
        ///     Перед отображением <see cref="TextField.Placeholder" /> вызывается данные метод, который указывает,
        ///     нужно ли воспринимать текущее значение свойства <see cref="TextBox.Text" /> как недействительное
        ///     и показывать вместо него <see cref="TextField.Placeholder" />.
        /// </param>
        public TextField(Func<string, bool> textValidationCallback) {
            this.textValidationCallback = textValidationCallback;
        }

        /// <summary>
        ///     Конструктор без параметров, а то без комментария так сразу и не поймешь.
        /// </summary>
        public TextField() : this(x => !string.IsNullOrWhiteSpace(x)) { }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);

            if (IsPlaceholderShowing) ShowPlaceholder(false);
        }

        protected override void OnGotFocus(RoutedEventArgs e) {
            HidePlaceholder();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e) {
            ShowPlaceholder(true);
            base.OnLostFocus(e);
        }

        #endregion Builders, On-methods

        #region Methods

        /// <summary>
        ///     Отображает заполнитель.
        /// </summary>
        /// <param name="askTextValidation">
        ///     Если передать <see cref="true" /> и переданный при инициализации экземпляра делегат вернет <see cref="true" />,
        ///     то заполнитель отображен не будет. Если передать <see cref="false" />, то метод для проверки действительности
        ///     текста вызываться не будет.
        /// </param>
        public void ShowPlaceholder(bool askTextValidation) {
            if (askTextValidation && textValidationCallback(Text)) return;

            SetValue(IsPlaceholderShowingProperty, true);
            originalForegroundBrush = Foreground;
            Foreground = PlaceholderForeground;
            base.Text = Placeholder;
        }

        /// <summary>
        ///     Скрывает заполнитель.
        /// </summary>
        public void HidePlaceholder() {
            if (!IsPlaceholderShowing) return;

            SetValue(IsPlaceholderShowingProperty, false);
            base.Text = string.Empty;
            Foreground = originalForegroundBrush;
        }

        #endregion Methods
    }
}