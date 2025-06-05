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
    /// Форма для поиска библиотечных карточек по различным критериям.
    /// </summary>
    public partial class SearchForm : Form
    {
        #region Поля

        /// <summary>
        /// Полный список всех библиотечных карточек, переданный из главной формы.
        /// Поиск будет осуществляться по этому списку.
        /// </summary>
        private readonly List<CardBase> _allCards;

        /// <summary>
        /// Источник данных для привязки результатов поиска к DataGridView.
        /// </summary>
        private readonly BindingSource _searchResultsBindingSource = new BindingSource();

        #endregion

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SearchForm"/>.
        /// </summary>
        /// <param name="cardsToSearch">Список всех библиотечных карточек, среди которых будет производиться поиск.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="cardsToSearch"/> равен null.</exception>
        public SearchForm(List<CardBase> cardsToSearch)
        {
            InitializeComponent();
            _allCards = cardsToSearch ?? throw new ArgumentNullException(nameof(cardsToSearch), "Список карточек для поиска не может быть null.");
            SetupResultsDataGridView();
        }

        #endregion

        #region Настройка UI

        /// <summary>
        /// Настраивает элемент DataGridView для отображения результатов поиска.
        /// Определяет колонки и их привязку к свойствам объектов CardBase.
        /// </summary>
        private void SetupResultsDataGridView()
        {
            searchResultsDataGridView.AutoGenerateColumns = false;
            searchResultsDataGridView.DataSource = _searchResultsBindingSource; // Привязка к BindingSource

            // Колонка "Тип"
            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn
            {
                Name = "searchTypeColumn", // Имя колонки для использования в CellFormatting
                HeaderText = "Тип",
                DataPropertyName = null, // Будет заполняться в CellFormatting
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(typeColumn);

            // Колонка "Фамилия"
            DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Фамилия",
                DataPropertyName = nameof(CardBase.Surname),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(surnameColumn);

            // Колонка "Имя"
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Имя",
                DataPropertyName = nameof(CardBase.Name),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(nameColumn);

            // Колонка "Название"
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                DataPropertyName = nameof(CardBase.Title),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            searchResultsDataGridView.Columns.Add(titleColumn);

            // Колонка "Год"
            DataGridViewTextBoxColumn yearColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Год",
                DataPropertyName = nameof(CardBase.Year),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            searchResultsDataGridView.Columns.Add(yearColumn);

            // Подписка на событие для форматирования ячейки "Тип"
            searchResultsDataGridView.CellFormatting += SearchResultsDataGridView_CellFormatting;
        }

        #endregion

        #region Обработчики событий UI

        /// <summary>
        /// Обрабатывает нажатие кнопки "Найти".
        /// Фильтрует список <see cref="_allCards"/> на основе введенных критериев
        /// и отображает результаты в <see cref="searchResultsDataGridView"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void performSearchButton_Click(object sender, EventArgs e)
        {
            // Получаем критерии поиска, приводя к нижнему регистру для регистронезависимого поиска
            // и удаляя лишние пробелы по краям.
            string surnameFilter = searchSurnameTextBox.Text.Trim().ToLowerInvariant();
            string nameFilter = searchNameTextBox.Text.Trim().ToLowerInvariant();
            string patronymicFilter = searchPatronymicTextBox.Text.Trim().ToLowerInvariant();
            string titleFilter = searchTitleTextBox.Text.Trim().ToLowerInvariant();
            string yearFilter = searchYearTextBox.Text.Trim(); // Год обычно ищут точным совпадением или "содержит" без изменения регистра

            // Начинаем с полного списка и последовательно применяем фильтры
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
                // Для отчества учитываем, что оно может быть пустым в данных
                query = query.Where(c => !string.IsNullOrEmpty(c.Patronymic) &&
                                         c.Patronymic.ToLowerInvariant().Contains(patronymicFilter));
            }
            // Если patronymicFilter пустой, то есть пользователь ничего не ввел в поле поиска отчества,
            // мы не фильтруем по нему (показывать и те, где отчество есть, и те, где его нет).
            // Если нужно искать карточки с ПУСТЫМ отчеством, можно добавить отдельный флажок "Без отчества".

            if (!string.IsNullOrEmpty(titleFilter))
            {
                query = query.Where(c => c.Title.ToLowerInvariant().Contains(titleFilter));
            }

            if (!string.IsNullOrEmpty(yearFilter))
            {
                // Для года можно использовать StartsWith, EndsWith, Equals или Contains
                // Contains - наиболее гибкий вариант, если пользователь вводит часть года.
                query = query.Where(c => c.Year.Contains(yearFilter));
            }

            List<CardBase> results = query.ToList();
            _searchResultsBindingSource.DataSource = results; // Устанавливаем отфильтрованный список как источник
            _searchResultsBindingSource.ResetBindings(false); // Обновляем DataGridView

            if (!results.Any())
            {
                MessageBox.Show(this, "Карточки, соответствующие критериям поиска, не найдены.", "Результаты поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Обрабатывает событие форматирования ячейки DataGridView для результатов поиска.
        /// Используется для динамического отображения типа карточки в соответствующей колонке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события форматирования ячейки.</param>
        private void SearchResultsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Проверяем, что это нужная колонка ("searchTypeColumn") и не заголовочная строка
            if (e.RowIndex >= 0 && searchResultsDataGridView.Columns[e.ColumnIndex].Name == "searchTypeColumn")
            {
                // Получаем объект CardBase, связанный с текущей строкой
                if (searchResultsDataGridView.Rows[e.RowIndex].DataBoundItem is CardBase card)
                {
                    e.Value = card.GetType().Name; // Устанавливаем имя типа класса как значение ячейки
                    e.FormattingApplied = true;    // Сообщаем, что форматирование выполнено
                }
            }
        }

        #endregion
    }
}
