using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms; // Добавляем для DataGridView

namespace LibraryView
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    //TODO: RSDN
    /// <summary>
    /// Методы расширения для DataGridView
    /// </summary>
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Создает текстовую колонку для DataGridView с общими настройками.
        /// </summary>
        public static DataGridViewTextBoxColumn CreateTextColumn(
            this DataGridView dataGridView,
            string headerText, string dataPropertyName,
            DataGridViewAutoSizeColumnMode autoSizeMode =
            DataGridViewAutoSizeColumnMode.AllCells)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                ReadOnly = true,
                AutoSizeMode = autoSizeMode
            };
        }

        /// <summary>
        /// Создает колонку для отображения типа карточки
        /// </summary>
        public static DataGridViewTextBoxColumn CreateTypeColumn(
            this DataGridView dataGridView, string columnName)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = columnName,
                HeaderText = "Тип",
                DataPropertyName = null,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
        }

        /// <summary>
        /// Форматирует ячейку типа карточки
        /// </summary>
        public static void FormatTypeCell(DataGridView dataGridView,
            DataGridViewCellFormattingEventArgs e, string typeColumnName)
        {
            if (e.RowIndex >= 0 &&
                dataGridView.Columns[e.ColumnIndex].Name == typeColumnName)
            {
                if (dataGridView.Rows[e.RowIndex].DataBoundItem is LibraryCards.CardBase card)
                {
                    e.Value = card.GetType().Name;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}