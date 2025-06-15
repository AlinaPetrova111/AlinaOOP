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
        /// Устанавливает значения по умолчанию 
        /// (минимум, максимум) для элементов NumericUpDown.
        /// </summary>
        private void SetupNumericUpDownDefaults()
        {
            //TODO: rewrite+
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
        /// Запускает процесс валидации и создания карточки.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateCommonFields();
                CreatedCard = CreateCardFromInput();

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
        /// Проверяет заполненность обязательных общих полей.
        /// </summary>
        /// <exception cref="ArgumentException">Если одно из полей не заполнено.</exception>
        private void ValidateCommonFields()
        {
            if (string.IsNullOrWhiteSpace(surnameTextBox.Text))
            {
                throw new ArgumentException("Фамилия автора не заполнена.");
            }
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                throw new ArgumentException("Имя автора не заполнено.");
            }
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                throw new ArgumentException("Название работы не заполнено.");
            }
            if (string.IsNullOrWhiteSpace(yearTextBox.Text))
            {
                throw new ArgumentException("Год издания не заполнен.");
            }
        }

        /// <summary>
        /// Создает объект карточки на основе выбранного типа.
        /// </summary>
        /// <returns>Созданный объект, унаследованный от <see cref="CardBase"/>.</returns>
        /// <exception cref="InvalidOperationException">Если выбран неизвестный тип карточки.</exception>
        private CardBase CreateCardFromInput()
        {
            var selectedCardType = _cardTypes[cardTypeComboBox.SelectedIndex];

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
            if (magazineEndSheetNumericUpDown.Value < magazineStartSheetNumericUpDown.Value)
            {
                throw new ArgumentException("Конечная страница не может быть меньше начальной.");
            }

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
            if (articleEndSheetNumericUpDown.Value < articleStartSheetNumericUpDown.Value)
            {
                throw new ArgumentException("Конечная страница не может быть меньше начальной.");
            }

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
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Случайные данные".
        /// Заполняет поля формы случайными корректными данными для выбранного типа карточки.
        /// </summary>
        private void createRandomDataButton_Click(object sender, EventArgs e)
        {
            CardBase randomCard = CardDataRandomizer.GenerateRandomCard(cardTypeComboBox.SelectedIndex);
            if (randomCard == null)
            {
                MessageBox.Show("Не удалось сгенерировать случайные данные для этого типа карточки.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            surnameTextBox.Text = randomCard.Surname;
            nameTextBox.Text = randomCard.Name;
            patronymicTextBox.Text = randomCard.Patronymic;
            titleTextBox.Text = randomCard.Title;
            yearTextBox.Text = randomCard.Year;

            if (randomCard is Book book)
            {
                bookPlaceOfPublicationTextBox.Text = book.PlaceOfPublication;
                bookPublishingHouseTextBox.Text = book.PublishingHouse;
                bookAdditionalInformationTextBox.Text = book.AdditionalInformation;
                bookSheetCountNumericUpDown.Value = Math.Max(bookSheetCountNumericUpDown.Minimum,
                    Math.Min(book.Sheet, bookSheetCountNumericUpDown.Maximum));
            }
            else if (randomCard is Magazine magazine)
            {
                magazineNameOfMagazineTextBox.Text = magazine.NameOfMagazine;
                magazineStartSheetNumericUpDown.Value = Math.Max(magazineStartSheetNumericUpDown.Minimum,
                    Math.Min(magazine.StartSheet, magazineStartSheetNumericUpDown.Maximum));
                magazineEndSheetNumericUpDown.Value = Math.Max(magazineEndSheetNumericUpDown.Minimum,
                    Math.Min(magazine.EndSheet, magazineEndSheetNumericUpDown.Maximum));
            }
            else if (randomCard is Article article)
            {
                articleNameOfCollectionTextBox.Text = article.NameOfArticle;
                articlePlaceOfPublicationTextBox.Text = article.PlaceOfPublication;
                articlePublishingHouseTextBox.Text = article.PublishingHouse;
                articleStartSheetNumericUpDown.Value = Math.Max(articleStartSheetNumericUpDown.Minimum,
                    Math.Min(article.StartSheet, articleStartSheetNumericUpDown.Maximum));
                articleEndSheetNumericUpDown.Value = Math.Max(articleEndSheetNumericUpDown.Minimum,
                    Math.Min(article.EndSheet, articleEndSheetNumericUpDown.Maximum));
            }
            else if (randomCard is Dissertation dissertation)
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
            }
        }
    }
}