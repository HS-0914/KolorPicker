using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolorPicker
{
    static class Program
    {
        static Mutex mutex;
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>

        [STAThread]
        static void Main()
        {
            mutex = new Mutex(true, "KolorPickerApp", out bool isNewInstance);

            if (!isNewInstance)
            {
                MessageBox.Show("KolorPicker는 이미 실행 중입니다.", "중복 실행 방지", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
