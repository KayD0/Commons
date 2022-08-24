using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.DataUtil
{
    public class UtilPager
    {
        #region メンバ変数
        public int CurrentPage { get; set; }

        public int DataCnt { get; set; }

        public int MaxDataInPage { get; set; }

        public int LastPageIndex { get; set; }

        public int LastPageDataCnt { get; set; }

        public int PageStartIndex { get; set; }

        public int PageEndIndex { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="dataCount"></param>
        /// <param name="maxDataInPage"></param>
        public UtilPager(int currentPage,int dataCount, int maxDataInPage) 
        {
            this.CurrentPage = currentPage;
            this.DataCnt = dataCount;
            this.MaxDataInPage = maxDataInPage;
            this.LastPageIndex = (int)(DataCnt / MaxDataInPage) + 1;
            this.LastPageDataCnt = DataCnt % MaxDataInPage;
            if (this.DataCnt == 0)
            {
                this.PageStartIndex = 0;
                this.PageEndIndex = 0;
            }
            else 
            {
                this.PageStartIndex = (this.MaxDataInPage * (this.CurrentPage - 1)) + 1;
                if (this.CurrentPage == this.LastPageIndex)
                {
                    this.PageEndIndex = this.LastPageDataCnt;
                }
                else 
                {
                    this.PageEndIndex = this.MaxDataInPage * this.CurrentPage;
                }
            }
        }
        #endregion

        #region ページの更新
        /// <summary>
        /// ページの更新
        /// </summary>
        /// <param name="selectIndex"></param>
        public void Update(int selectIndex) 
        {
            this.CurrentPage = selectIndex;
            if (this.DataCnt == 0)
            {
                this.PageStartIndex = 0;
                this.PageEndIndex = 0;
            }
            else
            {
                this.PageStartIndex = (this.MaxDataInPage * (this.CurrentPage - 1)) + 1;
                if (this.CurrentPage == this.LastPageIndex)
                {
                    this.PageEndIndex = this.LastPageDataCnt;
                }
                else
                {
                    this.PageEndIndex = this.MaxDataInPage * this.CurrentPage;
                }
            }
        }
        #endregion
    }
}
