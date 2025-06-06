using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryCards; 

namespace LibraryView
{
    /// <summary>
    /// Форма для добавления новой библиотечной карточки.
    /// Позволяет выбрать тип карточки и заполнить ее поля.
    /// </summary>
    public partial class AddCardForm : Form
    {

        /// <summary>
        /// Получает созданную на форме библиотечную карточку.
        /// Значение устанавливается после успешного нажатия кнопки "OK".
        /// </summary>
        public CardBase CreatedCard { get; private set; }

   
        /// <summary>
        /// Список панелей, содержащих поля, специфичные для каждого типа карточки.
        /// Порядок панелей должен соответствовать порядку 
        /// элементов в <see cref="cardTypeComboBox"/>.
        /// </summary>
        private List<Panel> _specificPanels;

        /// <summary>
        /// Генератор случайных чисел для заполнения полей тестовыми данными.
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// Список фамилий 
        /// </summary>
        private readonly List<string> _hpSurnames = new List<string>
        {
            "Поттер", "Грейнджер", "Уизли", "Малфой", "Лонгботтом", "Лавгуд",
            "Дамблдор", "Снейп", "Макгонагалл", "Блэк", "Люпин", "Волан-де-Морт"
        };

        /// <summary>
        /// Список мужских имен персонажей.
        /// </summary>
        private readonly List<string> _hpMaleNames = new List<string>
        {
            "Гарри", "Рон", "Драко", "Альбус", "Северус",
            "Сириус", "Римус", "Невилл", "Том"
        };

        /// <summary>
        /// Список женских имен персонажей.
        /// </summary>
        private readonly List<string> _hpFemaleNames = new List<string>
        {
            "Гермиона", "Джинни", "Луна", "Минерва", "Беллатриса",
            "Нимфадора", "Лили"
        };

        /// <summary>
        /// Список мужских отчеств.
        /// </summary>
        private readonly List<string> _hpMalePatronymics = new List<string>
        {
            "Джеймсович", "Артурович", "Люциусович", "Персивалевич",
            "Тобиасович",
            "Орионович", "Лайэллович", "Фрэнкович", "Реддлович"
        };

        /// <summary>
        /// Список женских отчеств.
        /// </summary>
        private readonly List<string> _hpFemalePatronymics = new List<string>
        {
            "Дэниеловна", "Артуровна", "Ксенофилиусовна", "Робертовна", "Эвановна"
        };

        /// <summary>
        /// Список названий для книг и статей.
        /// </summary>
        private readonly List<string> _hpBookTitles = new List<string>
        {
            "Современная зельеварение", "Тёмные искусства: Руководство",
            "История магии", "Квиддич сквозь века", "Фантастические твари" +
            " и где они обитают",
            "Тысяча магических трав и грибов", "Теория защитной магии"
        };

        /// <summary>
        /// Список названий для журналов.
        /// </summary>
        private readonly List<string> _hpMagazineTitles = new List<string>
        {
            "Придира", "Ежедневный пророк", "Ведьмин досуг", "Трансфигурация сегодня"
        };

        /// <summary>
        /// Список названий магических организаций.
        /// </summary>
        private readonly List<string> _hpOrganizations = new List<string>
        {
            "Министерство Магии", "Аврорат", "Отдел тайн", "Хогвартс", "Гринготтс"
        };

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddCardForm"/>.
        /// </summary>
        public AddCardForm()
        {
            InitializeComponent();
            InitializeCardTypeComboBox();
            InitializeSpecificPanelsList();
            SetupNumericUpDownDefaults(); 
            UpdateSpecificPanelVisibility(); 

#if !DEBUG
            createRandomDataButton.Visible = false;
#else
            createRandomDataButton.Visible = true;
#endif
        }


        /// <summary>
        /// Инициализирует ComboBox для выбора типа карточки.
        /// </summary>
        private void InitializeCardTypeComboBox()
        {
            cardTypeComboBox.Items.Clear();
            cardTypeComboBox.Items.Add("Книга");               
            cardTypeComboBox.Items.Add("Статья из журнала");  
            cardTypeComboBox.Items.Add("Статья из сборника"); 
            cardTypeComboBox.Items.Add("Диссертация");        
            cardTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList; 
            cardTypeComboBox.SelectedIndex = 0; 
            cardTypeComboBox.SelectedIndexChanged +=
                CardTypeComboBox_SelectedIndexChanged;
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
                bookSpecificPanel,          
                magazineSpecificPanel,      
                articleSpecificPanel,       
                dissertationSpecificPanel   
            };

            foreach (var panel in _specificPanels)
            {
                panel.Visible = false;
               // panel.Dock = DockStyle.Fill; 
            }
        }

        /// <summary>
        /// Устанавливает значения по умолчанию 
        /// (минимум, максимум) для элементов NumericUpDown.
        /// </summary>
        private void SetupNumericUpDownDefaults()
        {
            bookSheetCountNumericUpDown.Minimum = 1;
            bookSheetCountNumericUpDown.Maximum = 10000;
            bookSheetCountNumericUpDown.Value = 100; 

            magazineStartSheetNumericUpDown.Minimum = 1;
            magazineStartSheetNumericUpDown.Maximum = 9999;
            magazineStartSheetNumericUpDown.Value = 1;
            magazineEndSheetNumericUpDown.Minimum = 1;
            magazineEndSheetNumericUpDown.Maximum = 10000;
            magazineEndSheetNumericUpDown.Value = 10;

            articleStartSheetNumericUpDown.Minimum = 1;
            articleStartSheetNumericUpDown.Maximum = 9999;
            articleStartSheetNumericUpDown.Value = 1;
            articleEndSheetNumericUpDown.Minimum = 1;
            articleEndSheetNumericUpDown.Maximum = 10000;
            articleEndSheetNumericUpDown.Value = 10;

            dissertationSheetCountNumericUpDown.Minimum = 1;
            dissertationSheetCountNumericUpDown.Maximum = 10000;
            dissertationSheetCountNumericUpDown.Value = 150;
        }


        /// <summary>
        /// Обновляет видимость панелей со специфичными полями 
        /// в зависимости от выбранного типа карточки.
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
        }

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
             
                string surname = surnameTextBox.Text;
                string name = nameTextBox.Text;
                string patronymic = patronymicTextBox.Text; 
                string title = titleTextBox.Text;
                string year = yearTextBox.Text;

                
                if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Фамилия автора не заполнена.");
                if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя автора не заполнено.");
                if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Название работы не заполнено.");
                if (string.IsNullOrWhiteSpace(year)) throw new ArgumentException("Год издания не заполнен.");

                switch (cardTypeComboBox.SelectedIndex)
                {
                    case 0: 
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
                        book.AdditionalInformation = bookAdditionalInformationTextBox.Text; 
                        book.Sheet = (int)bookSheetCountNumericUpDown.Value;
                        CreatedCard = book;
                        break;

                    case 1: 
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

                    case 2: 
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

                    case 3: 
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
                        dissertation.SpecialtyCode = dissertationSpecialtyCodeTextBox.Text; 
                        dissertation.Organization = dissertationOrganizationTextBox.Text;
                        dissertation.NameOfSpeciality = dissertationNameOfSpecialityTextBox.Text;
                        dissertation.City = dissertationCityTextBox.Text;
                        dissertation.Sheet = (int)dissertationSheetCountNumericUpDown.Value;
                        CreatedCard = dissertation;
                        break;

                    default:
                        MessageBox.Show(this, "Неизвестный тип карточки выбран.", 
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                DialogResult = DialogResult.OK; 
                Close();
            }
            catch (ArgumentException ex) 
            {
                MessageBox.Show(this, $"Ошибка ввода: {ex.Message}", 
                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            catch (FormatException ex) 
            {
                MessageBox.Show(this, $"Ошибка формата числа: {ex.Message}", 
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(this, $"Произошла непредвиденная ошибка: " +
                    $"{ex.Message}\n{ex.StackTrace}", "Критическая ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Определяем пол для корректного выбора имени/отчества
            bool isMale = _random.Next(0, 2) == 0;

            surnameTextBox.Text = _hpSurnames[_random.Next(_hpSurnames.Count)];
            if (isMale)
            {
                nameTextBox.Text = _hpMaleNames[_random.Next(_hpMaleNames.Count)];
                // Отчество может отсутствовать
                patronymicTextBox.Text = _random.Next(0, 3) > 0
                    ? _hpMalePatronymics[_random.Next(_hpMalePatronymics.Count)]
                    : string.Empty;
            }
            else
            {
                nameTextBox.Text = _hpFemaleNames[_random.Next(_hpFemaleNames.Count)];
                patronymicTextBox.Text = _random.Next(0, 3) > 0
                    ? _hpFemalePatronymics[_random.Next(_hpFemalePatronymics.Count)]
                    : string.Empty;
            }

            titleTextBox.Text = _hpBookTitles[_random.Next(_hpBookTitles.Count)] + " №" + _random.Next(1, 100);
            yearTextBox.Text = _random.Next(1950, DateTime.Now.Year).ToString();

            switch (cardTypeComboBox.SelectedIndex)
            {
                case 0: // Книга
                    bookPlaceOfPublicationTextBox.Text = "Лондон";
                    bookPublishingHouseTextBox.Text = "Издательство 'Мракоборец'";
                    bookAdditionalInformationTextBox.Text = _random.Next(0, 2) == 0 ? "Расширенное издание" : "";
                    bookSheetCountNumericUpDown.Value = _random.Next(50, 1000);
                    break;
                case 1: // Статья из журнала
                    magazineNameOfMagazineTextBox.Text = _hpMagazineTitles[_random.Next(_hpMagazineTitles.Count)];
                    int startM = _random.Next(1, 50);
                    magazineStartSheetNumericUpDown.Value = startM;
                    magazineEndSheetNumericUpDown.Value = _random.Next(startM + 1, startM + 30);
                    break;
                case 2: // Статья из сборника
                    articleNameOfCollectionTextBox.Text = "Ежегодник заклинаний";
                    articlePlaceOfPublicationTextBox.Text = "Хогсмид";
                    articlePublishingHouseTextBox.Text = "Типография 'Флориш и Блоттс'";
                    int startA = _random.Next(1, 100);
                    articleStartSheetNumericUpDown.Value = startA;
                    articleEndSheetNumericUpDown.Value = _random.Next(startA + 1, startA + 25);
                    break;
                case 3: // Диссертация
                    dissertationKindOfDissertationTextBox.Text = "Магистерская";
                    dissertationBranchOfScienceTextBox.Text = "Защита от Тёмных искусств";
                    dissertationSpecialtyCodeTextBox.Text = $"{_random.Next(1, 10):D2}.{_random.Next(1, 10):D2}.{_random.Next(1, 10):D2}";
                    dissertationOrganizationTextBox.Text = _hpOrganizations[_random.Next(_hpOrganizations.Count)];
                    dissertationNameOfSpecialityTextBox.Text = "Боевая магия";
                    dissertationCityTextBox.Text = "Годрикова впадина";
                    dissertationSheetCountNumericUpDown.Value = _random.Next(100, 400);
                    break;
            }
        }
    }
}