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
    /// Форма для поиска библиотечных карточек по различным критериям.
    /// </summary>
    public partial class SearchForm : Form
    {
 
        /// <summary>
        /// Полный список всех библиотечных карточек, 
        /// переданный из главной формы.
        /// </summary>
        private readonly List<CardBase> _allCards;

        /// <summary>
        /// Источник данных для привязки результатов
        /// поиска к DataGridView.
        /// </summary>
        private readonly BindingSource _searchResultsBindingSource
            = new BindingSource();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SearchForm"/>.
        /// </summary>
        /// <param name="cardsToSearch">Список всех библиотечных карточек,
        /// среди которых будет производиться поиск.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается,
        /// если <paramref name="cardsToSearch"/> равен null.</exception>
        public SearchForm(List<CardBase> cardsToSearch)
        {
            InitializeComponent();
            //TODO: RSDN +
            _allCards = cardsToSearch ?? 
                throw new ArgumentNullException(nameof(cardsToSearch), 
                    "Список карточек для поиска не может быть null.");
            SetupResultsDataGridView();
        }

        /// <summary>
        /// Настраивает элемент DataGridView для отображения результатов поиска.
        /// Определяет колонки и их привязку к свойствам объектов CardBase.
        /// </summary>
        private void SetupResultsDataGridView()
        {
            searchResultsDataGridView.AutoGenerateColumns = false;
            searchResultsDataGridView.DataSource = _searchResultsBindingSource;
     
            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn
            {
                //TODO: duplication
                Name = "searchTypeColumn", 
                HeaderText = "Тип",
                DataPropertyName = null, 
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(typeColumn);

            //TODO: duplication
            DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Фамилия",
                DataPropertyName = nameof(CardBase.Surname),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(surnameColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Имя",
                DataPropertyName = nameof(CardBase.Name),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                DataPropertyName = nameof(CardBase.Title),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            searchResultsDataGridView.Columns.Add(titleColumn);

            DataGridViewTextBoxColumn yearColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Год",
                DataPropertyName = nameof(CardBase.Year),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(yearColumn);

            searchResultsDataGridView.CellFormatting += 
                SearchResultsDataGridView_CellFormatting;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Найти".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void performSearchButton_Click(object sender, EventArgs e)
        {
            string surnameFilter = searchSurnameTextBox.Text.Trim().ToLowerInvariant();
            string nameFilter = searchNameTextBox.Text.Trim().ToLowerInvariant();
            string patronymicFilter = searchPatronymicTextBox.Text.Trim().ToLowerInvariant();
            string titleFilter = searchTitleTextBox.Text.Trim().ToLowerInvariant();
            string yearFilter = searchYearTextBox.Text.Trim(); 
            
            IEnumerable<CardBase> query = _allCards;

            if (!string.IsNullOrEmpty(surnameFilter))
            {
                query = query.Where(c => c.Surname.ToLowerInvariant().Contains(surnameFilter));
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(c => c.Name.ToLowerInvariant().Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(patronymicFilter))
            {
                query = query.Where(c => !string.IsNullOrEmpty(c.Patronymic) && 
                c.Patronymic.ToLowerInvariant().Contains(patronymicFilter));
            }
  
            if (!string.IsNullOrEmpty(titleFilter))
            {
                query = query.Where(c => c.Title.ToLowerInvariant().Contains(titleFilter));
            }

            if (!string.IsNullOrEmpty(yearFilter))
            {
                query = query.Where(c => c.Year.Contains(yearFilter));
            }

            List<CardBase> results = query.ToList();
            _searchResultsBindingSource.DataSource = results; 
            _searchResultsBindingSource.ResetBindings(false); 

            if (!results.Any())
            {
                MessageBox.Show(this, "Карточки, соответствующие" +
                    " критериям поиска, не найдены.", "Результаты поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Закрыть".
        /// Закрывает форму поиска.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void closeSearchButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обрабатывает событие форматирования ячейки 
        /// DataGridView для результатов поиска.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события форматирования ячейки.</param>
        private void SearchResultsDataGridView_CellFormatting(object sender, 
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && 
                searchResultsDataGridView.Columns[e.ColumnIndex].Name 
                //TODO: duplication
                    == "searchTypeColumn")
            {
                if (searchResultsDataGridView.Rows[e.RowIndex].DataBoundItem is CardBase card)
                {
                    e.Value = card.GetType().Name; 
                    e.FormattingApplied = true;   
                }
            }
        }

    }
}
