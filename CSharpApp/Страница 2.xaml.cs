using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToursApp
{
    public partial class Страница_2 : Page
    {
        private string currentRole;

        public Страница_2()
        {
            InitializeComponent();
            currentRole = "Гость";
            LoadFilters();
            LoadData();
            SetAccessByRole(currentRole);
        }

        public Страница_2(string role)
        {
            InitializeComponent();
            currentRole = role;
            LoadFilters();
            LoadData();
            SetAccessByRole(currentRole);
        }

        private void LoadFilters()
        {
            using (var db = new Отдел_ОбразованияEntities())
            {
                var statuses = db.Статусы.ToList();
                statuses.Insert(0, new Статусы { id_Статуса = 0, Название = "Все статусы" });
                cbStatus.ItemsSource = statuses;
                cbStatus.SelectedValue = 0;

                var employees = db.Сотрудники.ToList();
                employees.Insert(0, new Сотрудники { id_Сотрудника = 0, ФИО = "Все сотрудники" });
                cbEmployee.ItemsSource = employees;
                cbEmployee.SelectedValue = 0;
            }
        }

       
        private void LoadData()
        {
            using (var db = new Отдел_ОбразованияEntities())
            {
                var query = db.Обращения.AsQueryable();

                string search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                    query = query.Where(o => o.Текст.Contains(search));

                if (cbStatus.SelectedValue != null && (int)cbStatus.SelectedValue > 0)
                    query = query.Where(o => o.id_Статуса == (int)cbStatus.SelectedValue);

                if (dpDateFrom.SelectedDate.HasValue)
                    query = query.Where(o => o.Дата_создания >= dpDateFrom.SelectedDate.Value);

                if (dpDateTo.SelectedDate.HasValue)
                    query = query.Where(o => o.Дата_создания <= dpDateTo.SelectedDate.Value);

                if (cbEmployee.SelectedValue != null && (int)cbEmployee.SelectedValue > 0)
                    query = query.Where(o => o.id_Сотрудника == (int)cbEmployee.SelectedValue);

                DGridU.ItemsSource = query.ToList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Страница_3());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGridU.SelectedItem == null)
            {
                MessageBox.Show("Выберите обращение");
                return;
            }

            Обращения обращение = (Обращения)DGridU.SelectedItem;
            Manager.MainFrame.Navigate(new Страница_3(обращение));
        }
        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new StatisticsPage());
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InfoPage());
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGridU.SelectedItem == null)
            {
                MessageBox.Show("Выберите обращение");
                return;
            }

            Обращения обращение = (Обращения)DGridU.SelectedItem;

            if (MessageBox.Show("Удалить обращение?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new Отдел_ОбразованияEntities())
                {
                    db.Обращения.Remove(обращение);
                    db.SaveChanges();
                }
                LoadData();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadData();
                SetAccessByRole(currentRole);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e) => LoadData();
        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            cbStatus.SelectedValue = 0;
            dpDateFrom.SelectedDate = null;
            dpDateTo.SelectedDate = null;
            cbEmployee.SelectedValue = 0;
            LoadData();
        }

        public void SetAccessByRole(string role)
        {
            switch (role)
            {
                case "Гость":
                    BtnAdd.Visibility = Visibility.Collapsed;
                    BtnDelete.Visibility = Visibility.Collapsed;
                    BtnStatistics.Visibility = Visibility.Collapsed;
                    BtnInfo.Visibility = Visibility.Visible;

                   
                    if (DGridU.Columns.Count > 0 && DGridU.Columns[DGridU.Columns.Count - 1] is DataGridTemplateColumn)
                    {
                        DGridU.Columns[DGridU.Columns.Count - 1].Visibility = Visibility.Collapsed;
                    }
                    break;

                case "Сотрудник":
                   
                    BtnAdd.Visibility = Visibility.Visible;
                    BtnDelete.Visibility = Visibility.Collapsed;
                    BtnStatistics.Visibility = Visibility.Collapsed;
                    BtnInfo.Visibility = Visibility.Visible;

                    
                    if (DGridU.Columns.Count > 0 && DGridU.Columns[DGridU.Columns.Count - 1] is DataGridTemplateColumn)
                    {
                        DGridU.Columns[DGridU.Columns.Count - 1].Visibility = Visibility.Visible;
                    }
                    break;

                case "Администратор":
              
                    BtnAdd.Visibility = Visibility.Visible;
                    BtnDelete.Visibility = Visibility.Visible;
                    BtnStatistics.Visibility = Visibility.Visible;
                    BtnInfo.Visibility = Visibility.Visible;

                   
                    if (DGridU.Columns.Count > 0 && DGridU.Columns[DGridU.Columns.Count - 1] is DataGridTemplateColumn)
                    {
                        DGridU.Columns[DGridU.Columns.Count - 1].Visibility = Visibility.Visible;
                    }
                    break;

                default:
                    goto case "Гость";
            }
        }
    }
}