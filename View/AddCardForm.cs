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
        /// Упорядоченный список типов карточек, используемый для заполнения ComboBox
        /// и сопоставления с панелями и методами создания.
        /// Порядок в этом списке является "источником истины".
        /// </summary>
        private readonly List<Type> _cardTypes = new List<Type>
        {
            typeof(Book),
            typeof(Magazine),
            typeof(Article),
            typeof(Dissertation)
        };

        /// <summary>
        /// Список панелей, содержащих поля, специфичные для каждого типа карточки.
        /// Порядок панелей должен соответствовать порядку типов в <see cref="_cardTypes"/>.
        /// </summary>
        private List<Panel> _specificPanels;

        // NEW: ErrorProvider для валидации и подсветки ошибок
        private ErrorProvider errorProvider;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddCardForm"/>.
        /// </summary>
        public AddCardForm()
        {
            InitializeComponent();
            InitializeCardTypeComboBox();
            InitializeSpecificPanelsList();
            UpdateSpecificPanelVisibility();

            // Инициализация ErrorProvider
            errorProvider = new ErrorProvider(this);
            errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError; 

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

            foreach (var cardType in _cardTypes)
            {
                if (Activator.CreateInstance(cardType) is CardBase instance)
                {
                    cardTypeComboBox.Items.Add(instance.GetTypeName());
                }
            }

            cardTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cardTypeComboBox.SelectedIndex = 0;
            cardTypeComboBox.SelectedIndexChanged +=
                CardTypeComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Инициализирует список панелей со специфичными полями.
        /// Важно, чтобы панели были добавлены в список в том же порядке,
        /// что и типы карточек в <see cref="_cardTypes"/>.
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
            }
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
        /// Запускает процесс валидации и создания карточки.
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            // CHANGED: Вместо try-catch, вызываем валидацию
            if (ValidateForm())
            {
                CreatedCard = CreateCardFromInput();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Исправьте ошибки в полях, выделенных красным.",
                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Метод полной валидации формы. Возвращает true, если все OK. Устанавливает ошибки.
        private bool ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear(); // Очищаем предыдущие ошибки

            // Валидация общих полей
            if (string.IsNullOrWhiteSpace(surnameTextBox.Text))
            {
                errorProvider.SetError(surnameTextBox, 
                    "Фамилия автора не заполнена.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProvider.SetError(nameTextBox, 
                    "Имя автора не заполнено.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                errorProvider.SetError(titleTextBox, 
                    "Название работы не заполнено.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(yearTextBox.Text))
            {
                errorProvider.SetError(yearTextBox,
                    "Год издания не заполнен.");
                isValid = false;
            }
            else
            {
                if (!int.TryParse(yearTextBox.Text, out int year) 
                    || year < 1000 || year > DateTime.Now.Year + 10)
                {
                    errorProvider.SetError(yearTextBox, 
                        "Год должен быть числом в диапазоне 1000–" + (DateTime.Now.Year + 10) + ".");
                    isValid = false;
                }
            }

            // Валидация специфических полей в зависимости от типа
            int selectedIndex = cardTypeComboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: // Book
                    if (string.IsNullOrWhiteSpace(bookPlaceOfPublicationTextBox.Text))
                    {
                        errorProvider.SetError(bookPlaceOfPublicationTextBox,
                            "Место издания не заполнено.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(bookPublishingHouseTextBox.Text))
                    {
                        errorProvider.SetError(bookPublishingHouseTextBox,
                            "Издательство не заполнено.");
                        isValid = false;
                    }
                    if (bookSheetCountNumericUpDown.Value <= 0)
                    {
                        errorProvider.SetError(bookSheetCountNumericUpDown,
                            "Количество страниц должно быть больше 0.");
                        isValid = false;
                    }
                    break;

                case 1: // Magazine
                    if (string.IsNullOrWhiteSpace(magazineNameOfMagazineTextBox.Text))
                    {
                        errorProvider.SetError(magazineNameOfMagazineTextBox, 
                            "Название журнала не заполнено.");
                        isValid = false;
                    }
                    if (magazineStartSheetNumericUpDown.Value <= 0)
                    {
                        errorProvider.SetError(magazineStartSheetNumericUpDown,
                            "Начальная страница должна быть больше 0.");
                        isValid = false;
                    }
                    if (magazineEndSheetNumericUpDown.Value <= magazineStartSheetNumericUpDown.Value)
                    {
                        errorProvider.SetError(magazineEndSheetNumericUpDown, 
                            "Конечная страница должна быть больше начальной.");
                        isValid = false;
                    }
                    break;

                case 2: // Article
                    if (string.IsNullOrWhiteSpace(articleNameOfCollectionTextBox.Text))
                    {
                        errorProvider.SetError(articleNameOfCollectionTextBox,
                            "Название сборника не заполнено.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(articlePlaceOfPublicationTextBox.Text))
                    {
                        errorProvider.SetError(articlePlaceOfPublicationTextBox,
                            "Место издания не заполнено.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(articlePublishingHouseTextBox.Text))
                    {
                        errorProvider.SetError(articlePublishingHouseTextBox,
                            "Издательство не заполнено.");
                        isValid = false;
                    }
                    if (articleStartSheetNumericUpDown.Value <= 0)
                    {
                        errorProvider.SetError(articleStartSheetNumericUpDown,
                            "Начальная страница должна быть больше 0.");
                        isValid = false;
                    }
                    if (articleEndSheetNumericUpDown.Value <= articleStartSheetNumericUpDown.Value)
                    {
                        errorProvider.SetError(articleEndSheetNumericUpDown, 
                            "Конечная страница должна быть больше начальной.");
                        isValid = false;
                    }
                    break;

                case 3: // Dissertation
                    if (string.IsNullOrWhiteSpace(dissertationKindOfDissertationTextBox.Text))
                    {
                        errorProvider.SetError(dissertationKindOfDissertationTextBox, 
                            "Вид диссертации не заполнен.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(dissertationBranchOfScienceTextBox.Text))
                    {
                        errorProvider.SetError(dissertationBranchOfScienceTextBox, 
                            "Отрасль науки не заполнена.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(dissertationSpecialtyCodeTextBox.Text))
                    {
                        errorProvider.SetError(dissertationSpecialtyCodeTextBox,
                            "Код специальности не заполнен.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(dissertationOrganizationTextBox.Text))
                    {
                        errorProvider.SetError(dissertationOrganizationTextBox,
                            "Организация не заполнена.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(dissertationNameOfSpecialityTextBox.Text))
                    {
                        errorProvider.SetError(dissertationNameOfSpecialityTextBox, 
                            "Название специальности не заполнено.");
                        isValid = false;
                    }
                    if (string.IsNullOrWhiteSpace(dissertationCityTextBox.Text))
                    {
                        errorProvider.SetError(dissertationCityTextBox,
                            "Город не заполнен.");
                        isValid = false;
                    }
                    if (dissertationSheetCountNumericUpDown.Value <= 0)
                    {
                        errorProvider.SetError(dissertationSheetCountNumericUpDown,
                            "Количество страниц должно быть больше 0.");
                        isValid = false;
                    }
                    break;
            }


            return isValid;
        }

        /// <summary>
        /// Создает объект карточки на основе выбранного типа.
        /// </summary>
        /// <returns>Созданный объект, унаследованный от <see cref="CardBase"/>.</returns>
        /// <exception cref="InvalidOperationException">Если выбран неизвестный 
        /// тип карточки.</exception>
        private CardBase CreateCardFromInput()
        {
            var selectedCardType = _cardTypes[cardTypeComboBox.SelectedIndex];

            //TODO: switch-case
            if (selectedCardType == typeof(Book))
            {
                return CreateBook();
            }
            if (selectedCardType == typeof(Magazine))
            {
                return CreateMagazine();
            }
            if (selectedCardType == typeof(Article))
            {
                return CreateArticle();
            }
            if (selectedCardType == typeof(Dissertation))
            {
                return CreateDissertation();
            }

            throw new InvalidOperationException("Неизвестный тип карточки выбран.");
        }

        /// <summary>
        /// Создает и заполняет объект типа "Книга".
        /// </summary>
        private Book CreateBook()
        {
            return new Book
            {
                Surname = surnameTextBox.Text,
                Name = nameTextBox.Text,
                Patronymic = patronymicTextBox.Text,
                Title = titleTextBox.Text,
                Year = yearTextBox.Text,
                PlaceOfPublication = bookPlaceOfPublicationTextBox.Text,
                PublishingHouse = bookPublishingHouseTextBox.Text,
                AdditionalInformation = bookAdditionalInformationTextBox.Text,
                Sheet = (int)bookSheetCountNumericUpDown.Value
            };
        }

        /// <summary>
        /// Создает и заполняет объект типа "Статья из журнала".
        /// </summary>
        private Magazine CreateMagazine()
        {
            return new Magazine
            {
                Surname = surnameTextBox.Text,
                Name = nameTextBox.Text,
                Patronymic = patronymicTextBox.Text,
                Title = titleTextBox.Text,
                Year = yearTextBox.Text,
                NameOfMagazine = magazineNameOfMagazineTextBox.Text,
                StartSheet = (int)magazineStartSheetNumericUpDown.Value,
                EndSheet = (int)magazineEndSheetNumericUpDown.Value
            };
        }

        /// <summary>
        /// Создает и заполняет объект типа "Статья из сборника".
        /// </summary>
        private Article CreateArticle()
        {
            return new Article
            {
                Surname = surnameTextBox.Text,
                Name = nameTextBox.Text,
                Patronymic = patronymicTextBox.Text,
                Title = titleTextBox.Text,
                Year = yearTextBox.Text,
                NameOfArticle = articleNameOfCollectionTextBox.Text,
                PlaceOfPublication = articlePlaceOfPublicationTextBox.Text,
                PublishingHouse = articlePublishingHouseTextBox.Text,
                StartSheet = (int)articleStartSheetNumericUpDown.Value,
                EndSheet = (int)articleEndSheetNumericUpDown.Value
            };
        }

        /// <summary>
        /// Создает и заполняет объект типа "Диссертация".
        /// </summary>
        private Dissertation CreateDissertation()
        {
            return new Dissertation
            {
                Surname = surnameTextBox.Text,
                Name = nameTextBox.Text,
                Patronymic = patronymicTextBox.Text,
                Title = titleTextBox.Text,
                Year = yearTextBox.Text,
                KindOfDissertation = dissertationKindOfDissertationTextBox.Text,
                BranchOfScience = dissertationBranchOfScienceTextBox.Text,
                SpecialtyCode = dissertationSpecialtyCodeTextBox.Text,
                Organization = dissertationOrganizationTextBox.Text,
                NameOfSpeciality = dissertationNameOfSpecialityTextBox.Text,
                City = dissertationCityTextBox.Text,
                Sheet = (int)dissertationSheetCountNumericUpDown.Value
            };
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Отмена".
        /// Закрывает форму с результатом <see cref="DialogResult.Cancel"/>.
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Случайные данные".
        /// Заполняет поля формы случайными корректными данными для выбранного типа карточки.
        /// </summary>
        private void CreateRandomDataButton_Click(object sender, EventArgs e)
        {
            //TODO: RSDN+
            CardBase randomCard =
                CardDataRandomizer.GenerateRandomCard(cardTypeComboBox.SelectedIndex);
            if (randomCard == null)
            {
                MessageBox.Show("Не удалось сгенерировать" +
                         " случайные данные для этого типа карточки.",
                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            surnameTextBox.Text = randomCard.Surname;
            nameTextBox.Text = randomCard.Name;
            patronymicTextBox.Text = randomCard.Patronymic;
            titleTextBox.Text = randomCard.Title;
            yearTextBox.Text = randomCard.Year;
            switch (randomCard)
            {
                //TODO: RSDN+
                case Book book:
                {
                bookPlaceOfPublicationTextBox.Text = book.PlaceOfPublication;
                bookPublishingHouseTextBox.Text = book.PublishingHouse;
                bookAdditionalInformationTextBox.Text = book.AdditionalInformation;
                bookSheetCountNumericUpDown.Value = Math.Max(bookSheetCountNumericUpDown.Minimum,
                    Math.Min(book.Sheet, bookSheetCountNumericUpDown.Maximum));
                    break;
                }
                case Magazine magazine:
                {
                magazineNameOfMagazineTextBox.Text = magazine.NameOfMagazine;
                magazineStartSheetNumericUpDown.Value = Math.Max(magazineStartSheetNumericUpDown.Minimum,
                    Math.Min(magazine.StartSheet, magazineStartSheetNumericUpDown.Maximum));
                magazineEndSheetNumericUpDown.Value = Math.Max(magazineEndSheetNumericUpDown.Minimum,
                    Math.Min(magazine.EndSheet, magazineEndSheetNumericUpDown.Maximum));
                    break;
                }
                case Article article:
                {
                articleNameOfCollectionTextBox.Text = article.NameOfArticle;
                articlePlaceOfPublicationTextBox.Text = article.PlaceOfPublication;
                articlePublishingHouseTextBox.Text = article.PublishingHouse;
                articleStartSheetNumericUpDown.Value = Math.Max(articleStartSheetNumericUpDown.Minimum,
                    Math.Min(article.StartSheet, articleStartSheetNumericUpDown.Maximum));
                articleEndSheetNumericUpDown.Value = Math.Max(articleEndSheetNumericUpDown.Minimum,
                    Math.Min(article.EndSheet, articleEndSheetNumericUpDown.Maximum));
                    break;
                }
                case Dissertation dissertation:
                {
                dissertationKindOfDissertationTextBox.Text = dissertation.KindOfDissertation;
                dissertationBranchOfScienceTextBox.Text = dissertation.BranchOfScience;
                dissertationSpecialtyCodeTextBox.Text = dissertation.SpecialtyCode;
                dissertationOrganizationTextBox.Text = dissertation.Organization;
                dissertationNameOfSpecialityTextBox.Text = dissertation.NameOfSpeciality;
                dissertationCityTextBox.Text = dissertation.City;
                dissertationSheetCountNumericUpDown.Value =
                    Math.Max(dissertationSheetCountNumericUpDown.Minimum,
                    Math.Min(dissertation.Sheet, dissertationSheetCountNumericUpDown.Maximum));
                    break;
                }
            }
        }
    }
}