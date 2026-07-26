using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToursApp
{
    public partial class Страница_3 : Page
    {
        private Отдел_ОбразованияEntities db = new Отдел_ОбразованияEntities();
        private Обращения _редактируемоеОбращение;

        public Страница_3(Обращения редактируемоеОбращение = null)
        {
            InitializeComponent();

            // Загрузка сотрудников
            CmbСотрудники.ItemsSource = db.Сотрудники.ToList();
            CmbСотрудники.DisplayMemberPath = "ФИО";
            CmbСотрудники.SelectedValuePath = "id_Сотрудника";

            // Загрузка учреждений
            CmbОбразовательные_учреждения.ItemsSource = db.Образовательные_учреждения.ToList();
            CmbОбразовательные_учреждения.DisplayMemberPath = "Название";
            CmbОбразовательные_учреждения.SelectedValuePath = "id_Образовательного_учреждения";

            _редактируемоеОбращение = редактируемоеОбращение;

            if (_редактируемоеОбращение != null)
            {
                TxtID.Text = _редактируемоеОбращение.id_Обращения.ToString();
                TxtName.Text = _редактируемоеОбращение.Текст;
                CmbСотрудники.SelectedValue = _редактируемоеОбращение.id_Сотрудника;
                CmbОбразовательные_учреждения.SelectedValue = _редактируемоеОбращение.id_Образовательного_учреждения;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка текста
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    MessageBox.Show("Введите текст обращения!", "Ошибка");
                    return;
                }

                // Проверка выбора сотрудника
                if (CmbСотрудники.SelectedValue == null)
                {
                    MessageBox.Show("Выберите сотрудника!", "Ошибка");
                    return;
                }

                // Проверка выбора учреждения
                if (CmbОбразовательные_учреждения.SelectedValue == null)
                {
                    MessageBox.Show("Выберите образовательное учреждение!", "Ошибка");
                    return;
                }

                if (_редактируемоеОбращение == null)
                {
                    Обращения новоеОбращение = new Обращения();
                    новоеОбращение.id_Обращения = Convert.ToInt32(TxtID.Text);
                    новоеОбращение.Текст = TxtName.Text;
                    новоеОбращение.Дата_создания = DateTime.Now;
                    новоеОбращение.id_Сотрудника = (int)CmbСотрудники.SelectedValue;
                    новоеОбращение.id_Образовательного_учреждения = (int)CmbОбразовательные_учреждения.SelectedValue;
                    новоеОбращение.id_Статуса = 1;
                    новоеОбращение.id_Жителя = 50;

                    db.Обращения.Add(новоеОбращение);
                    db.SaveChanges();
                    MessageBox.Show("Обращение добавлено!", "Успех");
                }
                else
                {
                    db.Обращения.Attach(_редактируемоеОбращение);
                    _редактируемоеОбращение.Текст = TxtName.Text;
                    _редактируемоеОбращение.id_Сотрудника = (int)CmbСотрудники.SelectedValue;
                    _редактируемоеОбращение.id_Образовательного_учреждения = (int)CmbОбразовательные_учреждения.SelectedValue;
                    db.SaveChanges();
                    MessageBox.Show("Обращение обновлено!", "Успех");
                }

                Manager.MainFrame.Navigate(new Страница_2());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                if (ex.InnerException != null)
                    MessageBox.Show($"Детали: {ex.InnerException.Message}");
            }
        }
        public void SetAccessByRole(string role)
        {
            if (role == "Гость")
            {
                BtnSave.Visibility = Visibility.Collapsed;
            }
        }
    }
}