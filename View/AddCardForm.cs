using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryCards; // Пространство имен вашего проекта с бизнес-логикой

namespace LibraryView
{
    /// <summary>
    /// Форма для добавления новой библиотечной карточки.
    /// Позволяет выбрать тип карточки и заполнить ее поля.
    /// </summary>
    public partial class AddCardForm : Form
    {
        #region Свойства

        /// <summary>
        /// Получает созданную на форме библиотечную карточку.
        /// Значение устанавливается после успешного нажатия кнопки "OK".
        /// </summary>
        public CardBase CreatedCard { get; private set; }

        #endregion

        #region Поля

        /// <summary>
        /// Список панелей, содержащих поля, специфичные для каждого типа карточки.
        /// Порядок панелей должен соответствовать порядку элементов в <see cref="cardTypeComboBox"/>.
        /// </summary>
        private List<Panel> _specificPanels;

        /// <summary>
        /// Генератор случайных чисел для заполнения полей тестовыми данными.
        /// </summary>
        private Random _random = new Random();

        #endregion

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddCardForm"/>.
        /// </summary>
        public AddCardForm()
        {
            InitializeComponent();
            InitializeCardTypeComboBox();
            InitializeSpecificPanelsList();
            SetupNumericUpDownDefaults(); // Установка мин/макс для NumericUpDown
            UpdateSpecificPanelVisibility(); // Показать панель для типа по умолчанию

            // Условная компиляция для кнопки случайных данных
#if !DEBUG
            createRandomDataButton.Visible = false;
#else
            createRandomDataButton.Visible = true; // Явно показываем в DEBUG
#endif
        }

        #endregion

        #region Инициализация UI

        /// <summary>
        /// Инициализирует ComboBox для выбора типа карточки.
        /// </summary>
        private void InitializeCardTypeComboBox()
        {
            cardTypeComboBox.Items.Clear();
            cardTypeComboBox.Items.Add("Книга");               // Индекс 0
            cardTypeComboBox.Items.Add("Статья из журнала");  // Индекс 1
            cardTypeComboBox.Items.Add("Статья из сборника"); // Индекс 2
            cardTypeComboBox.Items.Add("Диссертация");        // Индекс 3
            cardTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ввода текста
            cardTypeComboBox.SelectedIndex = 0; // Выбор "Книга" по умолчанию
            cardTypeComboBox.SelectedIndexChanged += CardTypeComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Инициализирует список панелей со специфичными полями.
        /// Важно, чтобы панели были добавлены в список в том же порядке,
        /// что и типы карточек в <see cref="cardTypeComboBox"/>.
        /// </summary>
        private void InitializeSpecificPanelsList()
        {
            _specificPanels = new List<Panel>
            {
                bookSpecificPanel,          // Для "Книга"
                magazineSpecificPanel,      // Для "Статья из журнала"
                articleSpecificPanel,       // Для "Статья из сборника"
                dissertationSpecificPanel   // Для "Диссертация"
            };

            // Убедимся, что все панели изначально скрыты, кроме той, что будет выбрана
            foreach (var panel in _specificPanels)
            {
                panel.Visible = false;
                panel.Dock = DockStyle.Fill; // Пример, как можно разместить панели, если они в общем контейнере
            }
        }

        /// <summary>
        /// Устанавливает значения по умолчанию (минимум, максимум) для элементов NumericUpDown.
        /// </summary>
        private void SetupNumericUpDownDefaults()
        {
            // Для книги
            bookSheetCountNumericUpDown.Minimum = 1;
            bookSheetCountNumericUpDown.Maximum = 10000;
            bookSheetCountNumericUpDown.Value = 100; // Значение по умолчанию

            // Для журнала
            magazineStartSheetNumericUpDown.Minimum = 1;
            magazineStartSheetNumericUpDown.Maximum = 9999;
            magazineStartSheetNumericUpDown.Value = 1;
            magazineEndSheetNumericUpDown.Minimum = 1;
            magazineEndSheetNumericUpDown.Maximum = 10000;
            magazineEndSheetNumericUpDown.Value = 10;

            // Для статьи из сборника
            articleStartSheetNumericUpDown.Minimum = 1;
            articleStartSheetNumericUpDown.Maximum = 9999;
            articleStartSheetNumericUpDown.Value = 1;
            articleEndSheetNumericUpDown.Minimum = 1;
            articleEndSheetNumericUpDown.Maximum = 10000;
            articleEndSheetNumericUpDown.Value = 10;

            // Для диссертации
            dissertationSheetCountNumericUpDown.Minimum = 1;
            dissertationSheetCountNumericUpDown.Maximum = 10000;
            dissertationSheetCountNumericUpDown.Value = 150;
        }


        /// <summary>
        /// Обновляет видимость панелей со специфичными полями в зависимости от выбранного типа карточки.
        /// </summary>
        private void UpdateSpecificPanelVisibility()
        {
            for (int i = 0; i < _specificPanels.Count; i++)
            {
                _specificPanels[i].Visible = (i == cardTypeComboBox.SelectedIndex);
                if (_specificPanels[i].Visible)
                {
                    _specificPanels[i].BringToFront();
                }
            }
            // Возможно, потребуется调整 размера формы или контейнера панелей, если они разные по высоте
            // this.ClientSize = new Size(this.ClientSize.Width, commonFieldsPanel.Bottom + _specificPanels[cardTypeComboBox.SelectedIndex].Height + okButton.Height + 30);
        }

        #endregion

        #region Обработчики событий UI

        /// <summary>
        /// Обрабатывает изменение выбранного элемента в ComboBox типов карточек.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void CardTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSpecificPanelVisibility();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "OK".
        /// Проводит валидацию введенных данных и, в случае успеха, создает объект карточки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем общие данные
                string surname = surnameTextBox.Text;
                string name = nameTextBox.Text;
                string patronymic = patronymicTextBox.Text; // Может быть пустым
                string title = titleTextBox.Text;
                string year = yearTextBox.Text;

                // Валидация общих полей (базовая, основная валидация в классах CardBase)
                if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Фамилия автора не заполнена.");
                if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя автора не заполнено.");
                if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Название работы не заполнено.");
                if (string.IsNullOrWhiteSpace(year)) throw new ArgumentException("Год издания не заполнен.");

                switch (cardTypeComboBox.SelectedIndex)
                {
                    case 0: // Книга
                        Book book = new Book
                        {
                            Surname = surname,
                            Name = name,
                            Patronymic = patronymic,
                            Title = title,
                            Year = year
                        };
                        book.PlaceOfPublication = bookPlaceOfPublicationTextBox.Text;
                        book.PublishingHouse = bookPublishingHouseTextBox.Text;
                        book.AdditionalInformation = bookAdditionalInformationTextBox.Text; // Свойство само обработает пустую строку
                        book.Sheet = (int)bookSheetCountNumericUpDown.Value;
                        CreatedCard = book;
                        break;

                    case 1: // Статья из журнала
                        if ((int)magazineEndSheetNumericUpDown.Value < (int)magazineStartSheetNumericUpDown.Value)
                            throw new ArgumentException("Конечная страница не может быть меньше начальной.");
                        Magazine magazine = new Magazine
                        {
                            Surname = surname,
                            Name = name,
                            Patronymic = patronymic,
                            Title = title,
                            Year = year
                        };
                        magazine.NameOfMagazine = magazineNameOfMagazineTextBox.Text;
                        magazine.StartSheet = (int)magazineStartSheetNumericUpDown.Value;
                        magazine.EndSheet = (int)magazineEndSheetNumericUpDown.Value;
                        CreatedCard = magazine;
                        break;

                    case 2: // Статья из сборника
                        if ((int)articleEndSheetNumericUpDown.Value < (int)articleStartSheetNumericUpDown.Value)
                            throw new ArgumentException("Конечная страница не может быть меньше начальной.");
                        Article article = new Article
                        {
                            Surname = surname,
                            Name = name,
                            Patronymic = patronymic,
                            Title = title,
                            Year = year
                        };
                        article.NameOfArticle = articleNameOfCollectionTextBox.Text;
                        article.PlaceOfPublication = articlePlaceOfPublicationTextBox.Text;
                        article.PublishingHouse = articlePublishingHouseTextBox.Text;
                        article.StartSheet = (int)articleStartSheetNumericUpDown.Value;
                        article.EndSheet = (int)articleEndSheetNumericUpDown.Value;
                        CreatedCard = article;
                        break;

                    case 3: // Диссертация
                        Dissertation dissertation = new Dissertation
                        {
                            Surname = surname,
                            Name = name,
                            Patronymic = patronymic,
                            Title = title,
                            Year = year
                        };
                        dissertation.KindOfDissertation = dissertationKindOfDissertationTextBox.Text;
                        dissertation.BranchOfScience = dissertationBranchOfScienceTextBox.Text;
                        dissertation.SpecialtyCode = dissertationSpecialtyCodeTextBox.Text; // Валидация формата внутри свойства
                        dissertation.Organization = dissertationOrganizationTextBox.Text;
                        dissertation.NameOfSpeciality = dissertationNameOfSpecialityTextBox.Text;
                        dissertation.City = dissertationCityTextBox.Text;
                        dissertation.Sheet = (int)dissertationSheetCountNumericUpDown.Value;
                        CreatedCard = dissertation;
                        break;

                    default:
                        MessageBox.Show(this, "Неизвестный тип карточки выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                DialogResult = DialogResult.OK; // Устанавливаем результат и закрываем форму
                Close();
            }
            catch (ArgumentException ex) // Ошибки валидации из свойств CardBase и его наследников, или из нашей проверки
            {
                MessageBox.Show(this, $"Ошибка ввода: {ex.Message}", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Можно добавить фокусировку на проблемном контроле, если это возможно определить
            }
            catch (FormatException ex) // Если бы использовали Parse для строк в числа
            {
                MessageBox.Show(this, $"Ошибка формата числа: {ex.Message}", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Другие непредвиденные ошибки
            {
                MessageBox.Show(this, $"Произошла непредвиденная ошибка: {ex.Message}\n{ex.StackTrace}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Отмена".
        /// Закрывает форму с результатом <see cref="DialogResult.Cancel"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Случайные данные".
        /// Заполняет поля формы случайными корректными данными для выбранного типа карточки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void createRandomDataButton_Click(object sender, EventArgs e)
        {
            // Общие поля
            surnameTextBox.Text = GetRandomRussianNamePart(true);
            nameTextBox.Text = GetRandomRussianNamePart(false);
            patronymicTextBox.Text = _random.Next(0, 2) == 0 ? GetRandomRussianNamePart(false) : ""; // 50% шанс на отчество
            titleTextBox.Text = "Случайное Название Работы №" + _random.Next(1, 1000);
            yearTextBox.Text = _random.Next(1950, DateTime.Now.Year).ToString();

            // Специфичные поля в зависимости от типа
            switch (cardTypeComboBox.SelectedIndex)
            {
                case 0: // Книга
                    bookPlaceOfPublicationTextBox.Text = "Город " + _random.Next(1, 50);
                    bookPublishingHouseTextBox.Text = "Издательство '" + GetRandomWord() + "'";
                    bookAdditionalInformationTextBox.Text = _random.Next(0, 3) == 0 ? "Переиздание" : "";
                    bookSheetCountNumericUpDown.Value = _random.Next(50, 1000);
                    break;
                case 1: // Статья из журнала
                    magazineNameOfMagazineTextBox.Text = "Журнал '" + GetRandomWord() + " Науки'";
                    int startM = _random.Next(1, 50);
                    magazineStartSheetNumericUpDown.Value = startM;
                    magazineEndSheetNumericUpDown.Value = Math.Max(startM, _random.Next(startM, startM + 30));
                    break;
                case 2: // Статья из сборника
                    articleNameOfCollectionTextBox.Text = "Сборник трудов '" + GetRandomWord() + "'";
                    articlePlaceOfPublicationTextBox.Text = "Город " + _random.Next(1, 20);
                    articlePublishingHouseTextBox.Text = "Университетское изд-во";
                    int startA = _random.Next(1, 100);
                    articleStartSheetNumericUpDown.Value = startA;
                    articleEndSheetNumericUpDown.Value = Math.Max(startA, _random.Next(startA, startA + 25));
                    break;
                case 3: // Диссертация
                    dissertationKindOfDissertationTextBox.Text = _random.Next(0, 2) == 0 ? "Кандидатская" : "Докторская";
                    dissertationBranchOfScienceTextBox.Text = "Технические науки"; // Пример
                    dissertationSpecialtyCodeTextBox.Text = $"{_random.Next(1, 99):D2}.{_random.Next(1, 99):D2}.{_random.Next(1, 99):D2}";
                    dissertationOrganizationTextBox.Text = "НИИ '" + GetRandomWord() + "'";
                    dissertationNameOfSpecialityTextBox.Text = "Специальность " + GetRandomWord();
                    dissertationCityTextBox.Text = "Наукоград";
                    dissertationSheetCountNumericUpDown.Value = _random.Next(100, 400);
                    break;
            }
        }

        #endregion

        #region Вспомогательные методы для случайных данных

        /// <summary>
        /// Генерирует случайную часть русского ФИО (фамилия или имя/отчество).
        /// </summary>
        /// <param name="isSurname">True, если генерируется фамилия (обычно длиннее), иначе false.</param>
        /// <returns>Случайная строка, похожая на часть ФИО.</returns>
        private string GetRandomRussianNamePart(bool isSurname)
        {
            string[] firstSyllables = { "Ив", "Петр", "Сид", "Куз", "Смир", "Поп", "Вас", "Мих", "Ал", "Серг", "Добр", "Люд" };
            string[] middleSyllables = { "ан", "ов", "ев", "ин", "ск", "енк", "ай", "ей", "ий", "он", "ен", "ар" };
            string[] lastSyllablesMale = { "ов", "ев", "ин", "ский", "енко", "ич", "ко" };
            string[] lastSyllablesFemale = { "ова", "ева", "ина", "ская", "енко", "на", "ая" };

            bool isMale = _random.Next(0, 2) == 0;
            string[] currentLastSyllables = isMale ? lastSyllablesMale : lastSyllablesFemale;

            string name = firstSyllables[_random.Next(firstSyllables.Length)];
            if (!isSurname || _random.Next(0, 2) == 0) // Для имен короче
            {
                name += middleSyllables[_random.Next(middleSyllables.Length)];
            }
            name += currentLastSyllables[_random.Next(currentLastSyllables.Length)];
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// Генерирует случайное слово.
        /// </summary>
        /// <returns>Случайное слово.</returns>
        private string GetRandomWord()
        {
            string[] words = { "Прогресс", "Развитие", "Анализ", "Синтез", "Методика", "Исследование", "Инновация", "Технология" };
            return words[_random.Next(words.Length)];
        }

        #endregion
    }
}