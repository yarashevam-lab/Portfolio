using System;
using System.Linq;
using System.Windows.Controls;

namespace ToursApp
{
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            using (var db = new Отдел_ОбразованияEntities())
            {
                int totalCount = db.Обращения.Count();
                tbTotalCount.Content = totalCount.ToString();

                var byStatus = db.Обращения
                    .GroupBy(o => o.Статусы.Название)
                    .Select(g => new { Status = g.Key, Count = g.Count() })
                    .ToList();
                lbByStatus.ItemsSource = byStatus.Select(x => $"{x.Status}: {x.Count}");

                var byEmployee = db.Обращения
                    .Where(o => o.Сотрудники != null)
                    .GroupBy(o => o.Сотрудники.ФИО)
                    .Select(g => new { Employee = g.Key, Count = g.Count() })
                    .ToList();
                lbByEmployee.ItemsSource = byEmployee.Select(x => $"{x.Employee}: {x.Count}");
            }
        }
    }
}