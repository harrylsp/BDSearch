using System.Windows.Data;
using System.Windows.Media;

namespace BDSearch.Converter
{
    public class FStatusConverter : IValueConverter
    {

        /// <summary>
        /// 方法
        /// </summary>
        /// <param name="value">参数1</param>
        /// <param name="targetType">参数2</param>
        /// <param name="parameter">参数3</param>
        /// <param name="culture">参数4</param>
        /// <returns>返回值</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var status = value?.ToString();
                if (status == "1")
                {
                    return "已下载";
                }
                else
                {
                    return "未下载";
                }
            }
            catch
            {
                return "未下载";
            }
        }
        /// <summary>
        /// 方法
        /// </summary>
        /// <param name="value">参数1</param>
        /// <param name="targetType">参数2</param>
        /// <param name="parameter">参数3</param>
        /// <param name="culture">参数4</param>
        /// <returns>返回值</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "未下载";
        }

    }
}

