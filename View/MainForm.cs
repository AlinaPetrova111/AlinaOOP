using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// Имя колонки для отображения типа карточки.
        /// </summary>
        private const string TypeColumnName = "typeColumn";

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
        /// Создает текстовую колонку для DataGridView с общими настройками.
        /// </summary>
        /// <param name="headerText">Текст заголовка колонки.</param>
        /// <param name="dataPropertyName">Имя свойства для привязки данных.</param>
        /// <param name="autoSizeMode">Режим автоматического изменения размера колонки.</param>
        /// <returns>Готовый объект DataGridViewTextBoxColumn.</returns>
        private DataGridViewTextBoxColumn CreateTextColumn(string headerText, string dataPropertyName,
            DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.AllCells)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                ReadOnly = true,
                AutoSizeMode = autoSizeMode
            };
        }

        //TODO: duplication+
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
                Name = TypeColumnName,
                HeaderText = "Тип",
                DataPropertyName = null,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            cardsDataGridView.Columns.Add(typeColumn);
            cardsDataGridView.Columns.Add(
                CreateTextColumn("Фамилия", nameof(CardBase.Surname)));
            cardsDataGridView.Columns.Add(
                CreateTextColumn("Имя", nameof(CardBase.Name)));
            cardsDataGridView.Columns.Add(
                CreateTextColumn("Название", nameof(CardBase.Title), DataGridViewAutoSizeColumnMode.Fill));
            cardsDataGridView.Columns.Add(
                CreateTextColumn("Год", nameof(CardBase.Year)));

            cardsDataGridView.CellFormatting += cardsDataGridView_CellFormatting;
        }

        /// <summary>
        /// Возвращает массив типов, известных сериализатору, 
        /// автоматически находя все классы, унаследованные от CardBase.
        /// </summary>
        /// <returns>Массив типов.</returns>
        private Type[] GetKnownTypes()
        {
            return Assembly.GetAssembly(typeof(CardBase))
                           .GetTypes()
                           .Where(type => type.IsSubclassOf(typeof(CardBase)) && !type.IsAbstract)
                           .ToArray();
        }

        /// <summary>
        /// Обновляет данные в DataGridView, перечитывая их из источника.
        /// </summary>
        private void RefreshGrid()
        {
            _bindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Обрабатывает событие форматирования ячейки DataGridView.
        /// Используется для отображения имени типа в специальной колонке.
        /// </summary>
        private void cardsDataGridView_CellFormatting(object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                cardsDataGridView.Columns[e.ColumnIndex].Name == TypeColumnName)
            {
                if (cardsDataGridView.Rows[e.RowIndex].DataBoundItem is CardBase card)
                {
                    e.Value = card.GetTypeName();
                    e.FormattingApplied = true;
                }
            }
        }

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

        private void searchCardButton_Click(object sender, EventArgs e)
        {
            using (SearchForm searchForm = new SearchForm(_cards))
            {
                searchForm.ShowDialog(this);
            }
        }

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