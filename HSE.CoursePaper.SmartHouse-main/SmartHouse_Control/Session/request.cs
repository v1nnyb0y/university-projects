using System.Collections.Generic;
using System.Data;
using SmartHouse_Control.Handlers;

namespace SmartHouse_Control.Session
{
    public class request
    {
        #region Initialize

        public request() {
            do_new_qq = subscribe.generated_select;
        }

        #endregion

        public DataTable new_qq(List<object> select, List<object> from, List<object[]> where) {
            var qq = "";
            if (where == null)
                qq = string.Concat(
                    this.select,
                    get_select(select),
                    this.from,
                    get_from(from)
                );
            else
                qq = string.Concat(
                    this.select,
                    get_select(select),
                    this.from,
                    get_from(from),
                    this.where,
                    get_where(where)
                );

            return OnDo(qq);
        }

        #region Fields

        private readonly string select = "SELECT TOP 10000 "; //what select
        private readonly string from = "FROM "; //From what select
        private readonly string where = "WHERE "; //Where select
        private int number_where = 0;

        #endregion

        #region Generator

        #region What select

        /// <summary>
        ///     SELECT PART GENERATOR
        /// </summary>
        /// <param name="arr_selects"></param>
        /// <returns></returns>
        private string get_select(List<object> arr_selects) {
            var result = "";
            foreach (var item in arr_selects) {
                var arr = (item as string).Split('.');
                result = string.Concat(result, '[', arr[0], "].[", arr[1], ']', ',', ' ');
            }

            result = result.Remove(result.Length - 2, 1);

            return result;
        }

        #endregion

        #region From what select

        /// <summary>
        ///     FROM PART GENERATOR
        /// </summary>
        /// <param name="arr_from"></param>
        /// <returns></returns>
        private string get_from(List<object> arr_from) {
            var result = "";
            foreach (var item in arr_from) {
                var str = item as string;

                if (str == "Option" || str == "User") str = string.Concat('[', str, ']');

                result = string.Concat(result, str, ',', ' ');
            }

            result = result.Remove(result.Length - 2, 1);

            return result;
        }

        #endregion

        #region Conditions

        /// <summary>
        ///     WHERE PART GENERATOR
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        private string get_where(List<object[]> wheres) {
            var result = "(";
            foreach (var item in wheres)
                if (item[3] == "")
                    result = string.Concat(result, get_condition(item));
                else
                    result = string.Concat(result, get_condition(item), ' ', item[3]);

            result = string.Concat(result, ")");
            return result;
        }

        /// <summary>
        ///     GET EACH CONDITION
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string get_condition(object[] condition) {
            var result = "(";

            var arr = (condition[0] as string).Split('.');
            result = string.Concat(result, '[', arr[0], "].[", arr[1], ']');
            result = string.Concat(result, ' ', condition[1], ' ');

            if ((bool) condition[4]) {
                result = string.Concat(result, "'", condition[2], "'", ")");
            }
            else {
                arr = (condition[2] as string).Split('.');
                result = string.Concat(result, '[', arr[0], "].[", arr[1], ']', ')');
            }

            return result;
        }

        #endregion

        #endregion

        #region Delegates

        private delegate DataTable generator(string select);

        private event generator do_new_qq;

        private DataTable OnDo(string select) {
            return do_new_qq?.Invoke(select);
        }

        #endregion
    }
}