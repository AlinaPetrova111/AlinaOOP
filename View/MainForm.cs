using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using LibraryCards;

namespace LibraryView
{
    /// <summary>
    /// Главная форма приложения для 
    /// управления списком библиотечных карточек.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Список всех библиотечных карточек.
        /// </summary>
        private List<CardBase> _cards = new List<CardBase>();

        /// <summary>
        /// Источник данных для привязки списка карточек к DataGridView.
        /// </summary>
        private BindingSource _bindingSource = new BindingSource();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        /// <summary>
        /// Настраивает элемент DataGridView для отображения карточек.
        /// Определяет колонки и их привязку к свойствам объектов CardBase.
        /// </summary>
        private void SetupDataGridView()
        {
            cardsDataGridView.AutoGenerateColumns = false;
            _bindingSource.DataSource = _cards;
            cardsDataGridView.DataSource = _bindingSource;

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn
            {
                Name = "typeColumn", 
                HeaderText = "Тип",
                DataPropertyName = null, 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            cardsDataGridView.Columns.Add(typeColumn);

            DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Фамилия",
                DataPropertyName = nameof(CardBase.Surname), 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            cardsDataGridView.Columns.Add(surnameColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Имя",
                DataPropertyName = nameof(CardBase.Name), 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            cardsDataGridView.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                DataPropertyName = nameof(CardBase.Title), 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill 
            };
            cardsDataGridView.Columns.Add(titleColumn);

            DataGridViewTextBoxColumn yearColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Год",
                DataPropertyName = nameof(CardBase.Year), 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            cardsDataGridView.Columns.Add(yearColumn);

            cardsDataGridView.CellFormatting += cardsDataGridView_CellFormatting;
        }

        /// <summary>
        /// Обновляет данные в DataGridView, перечитывая их из источника.
        /// </summary>
        private void RefreshGrid()
        {
            _bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку добавления новой карточки.
        /// Открывает диалоговое окно <see cref="AddCardForm"/> для создания карточки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void addCardButton_Click(object sender, EventArgs e)
        {
            using (AddCardForm addForm = new AddCardForm())
            {
                if (addForm.ShowDialog(this) == DialogResult.OK) 
                {
                    if (addForm.CreatedCard != null)
                    {
                        _cards.Add(addForm.CreatedCard);
                        RefreshGrid();
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку удаления выбранной карточки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void removeCardButton_Click(object sender, EventArgs e)
        {
            if (cardsDataGridView.CurrentRow != null
                && cardsDataGridView.CurrentRow.DataBoundItem is CardBase selectedCard)
            {
                var confirmResult = MessageBox.Show(this, $"Вы уверены, что " +
                    $"хотите удалить карточку: {selectedCard.Title}?",
                                     "Подтверждение удаления",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    _cards.Remove(selectedCard);
                    RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show(this, "Пожалуйста, выберите" +
                    " карточку для удаления.", "Удаление невозможно", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку поиска карточек.
        /// Открывает диалоговое окно <see cref="SearchForm"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void searchCardButton_Click(object sender, EventArgs e)
        {
            using (SearchForm searchForm = new SearchForm(_cards))
            {
                searchForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Обрабатывает событие форматирования ячейки DataGridView.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события форматирования ячейки.</param>
        private void cardsDataGridView_CellFormatting(object sender, 
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && 
                cardsDataGridView.Columns[e.ColumnIndex].Name == "typeColumn")
            {
                if (cardsDataGridView.Rows[e.RowIndex].DataBoundItem is CardBase card)
                {
                    e.Value = card.GetType().Name; 
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Возвращает массив типов, известных сериализатору (все производные от CardBase).
        /// </summary>
        /// <returns>Массив типов.</returns>
        private Type[] GetKnownTypes()
        {
            return new Type[] { typeof(Book), typeof(Magazine), 
                typeof(Article), typeof(Dissertation) };
        }

        /// <summary>
        /// Обрабатывает выбор пункта меню "Сохранить как...".
        /// Сохраняет текущий список карточек в XML-файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Файлы библиотечных карточек" +
                    " (*.libcard)|*.libcard|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить список карточек";
                saveFileDialog.DefaultExt = "libcard";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<CardBase>), GetKnownTypes());
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            serializer.Serialize(writer, _cards);
                        }
                        MessageBox.Show(this, "Данные успешно сохранены!", 
                            "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Ошибка при сохранении данных: " +
                            $"{ex.Message}\n{ex.StackTrace}", "Ошибка сохранения", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает выбор пункта меню "Открыть...".
        /// Загружает список карточек из XML-файла.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Файлы библиотечных карточек" +
                    " (*.libcard)|*.libcard|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Открыть список карточек";
                openFileDialog.DefaultExt = "libcard";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<CardBase>), GetKnownTypes());
                        using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                        {
                            var loadedCards = serializer.Deserialize(reader) as List<CardBase>;
                            if (loadedCards != null)
                            {
                                _cards.Clear();
                                _cards.AddRange(loadedCards);
                                RefreshGrid();
                                MessageBox.Show(this, "Данные успешно загружены!",
                                    "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(this, "Не удалось загрузить данные." +
                                    " Файл может быть поврежден или иметь неверный формат.", 
                                    "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Ошибка при загрузке данных: " +
                            $"{ex.Message}\n{ex.StackTrace}", "Ошибка загрузки", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
