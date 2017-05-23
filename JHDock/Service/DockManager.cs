using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using JHDock.Properties;
using System.Configuration;

namespace JHDock
{
    /// <summary>
    /// 系统图标操作s
    /// </summary>
    public class DockManager
    {

        private Collection m_DockItems;
        /// <summary>
        /// 构造时初使化
        /// </summary>
        public DockManager()
        {
            m_DockItems = new Collection();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Key"></param>
        /// <param name="Before"></param>
        /// <param name="After"></param>
        public void Add(IDockItem Item, string Key = null, IDockItem Before = null, IDockItem After = null)
        {
            m_DockItems.Add(Item, Key, Before, After);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="Key"></param>
        public void Remove(string Key)
        {
            m_DockItems.Remove(Key);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="Index"></param>
        public void Remove(int Index)
        {
            m_DockItems.Remove(Index);
        }
        /// <summary>
        /// 返回图标个数
        /// </summary>
        public long DockCount
        {
            get { return m_DockItems.Count; }
        }
        /// <summary>
        /// 根据索引获取图标信息
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IDockItem get_Item(int Index)
        {
            return ((JHDock.IDockItem)(m_DockItems[Index]));
        }
        /// <summary>
        /// 根据图标获取完整信息
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IDockItem get_Item(IDockItem Index)
        {
            return ((JHDock.IDockItem)(m_DockItems[Index]));

        }

        //public IDockItem Item( int Index )
        //{
        //    get { return ((JHDock.IDockItem)m_DockItems[Index]); }
        //}

        //public IDockItem Item( IDockItem Index )
        //{
        //    get { return ((JHDock.IDockItem)m_DockItems[Index]); }
        //}
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return m_DockItems.GetEnumerator();
        }
        /// <summary>
        /// 点击图标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Onclick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            IDockItem pDockItem = null;
            foreach (IDockItem pDockItem_loopVariable in m_DockItems)
            {
                pDockItem = pDockItem_loopVariable;
                if (e.X >= pDockItem.X & e.X <= (pDockItem.X + pDockItem.Width))
                {
                    if (e.Y >= pDockItem.Y & e.Y <= (pDockItem.Y + pDockItem.Height))
                    {
                        //毕兰
                        //pDockItem.Onclick(sender, e);
                        pDockItem.LockItem = true;
                        pDockItem.Onclick(pDockItem, e);


                        //进入系统时，左侧栏自动缩小到任务栏中即可
                        DockWindow dw = sender as DockWindow;
                        dw.Hide();
                        dw.noTiIc.Visible = true;
                    }
                }
            }
        }
        public void MouseLeve(object sender, System.Windows.Forms.MouseEventArgs e,FrmSubSystem frmSubSystem)
        {
            IDockItem pDockItem = null;
            foreach (IDockItem pDockItem_loopVariable in m_DockItems)
            {
                pDockItem = pDockItem_loopVariable;
                if (e.X >= pDockItem.X & e.X <= (pDockItem.X + pDockItem.Width))
                {
                    if (e.Y >= pDockItem.Y & e.Y <= (pDockItem.Y + pDockItem.Height))
                    {
                        frmSubSystem.Cursor = Cursors.Hand;
                        break;
                    }
                    else
                    {
                        frmSubSystem.Cursor = Cursors.Arrow;
                    }
                }
                else
                {
                    frmSubSystem.Cursor = Cursors.Arrow;
                }
            }
        }
        /// <summary>
        /// 初使化
        /// </summary>
        /// <param name="DockSettings"></param>
        public void Initilize(Settings DockSettings, int pWidth)
        {
            IDockItem pDockItem = null;
            int pZoom = 0;
            //2013-12-06 庞少军 修改成靠右
            //int pX = 0;
            //pX = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
            int pY = 0;
            pY = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterTopMargin;
            foreach (IDockItem pDockItem_loopVariable in m_DockItems)
            {
                pDockItem = pDockItem_loopVariable;
                //pDockItem.Y = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterTopMargin;
                //pDockItem.X = pX;
                pDockItem.X = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
                pDockItem.Y = pY;
                pZoom = (int)DockSettings.Icons.SizePx;
                pDockItem.Width = pZoom;
                pDockItem.Height = pZoom;
                //pDockItem.InitilizeCentre(pX + pZoom / 2, pDockItem.Y + pZoom / 2);
                //pX = pX + pDockItem.Width + 10;
                pDockItem.InitilizeCentre(pDockItem.X + pZoom / 2, pY + pZoom / 2);
                pDockItem.RightX = pDockItem.X + pZoom;
                pY = pY + pDockItem.Height + 10;
            }
        }
        /// <summary>
        /// 2014-6-06 陈宝栋创建
        /// 绘制子系统窗体的方法
        /// </summary>
        /// <param name="DstGraphics"></param>
        /// <param name="DockSettings"></param>
        /// <param name="CurX"></param>
        /// <param name="CurY"></param>
        public void Paint1(Graphics DstGraphics, Settings DockSettings,int icoinX,int incoinY, float CurX = 0, float CurY = 0, bool isStop = false)
        {
            IDockItem pDockItem = null;
            int pZoom = 0;
            int pY = 0;
            int pMaxY = 0;
            pY = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterTopMargin;
            pMaxY = (int)(DockSettings.Icons.ZoomPx * DockSettings.Icons.ZoomWidth) / 2;
            int countX = 0;
            int countY = 0;
            foreach (IDockItem pDockItem_loopVariable in m_DockItems)
            {

                pDockItem = pDockItem_loopVariable;
                //如果图片数量大于5，则换行
                if (countX >=5)
                {
                    countX = 0;
                    countY++;
                }
                pDockItem.X = icoinX + countX * (72+40);
                countX++;
                Font drawFont = new Font("微软雅黑", 11, FontStyle.Regular);
                SizeF drawSize = DstGraphics.MeasureString("微软雅黑", drawFont);
                if (countY > 0)
                {
                    pDockItem.Y = (int)(incoinY + countY * 72 + drawSize.Height);
                }
                else
                {
                    pDockItem.Y = incoinY + countY * 72;
                }
                pZoom = (int)DockSettings.Icons.SizePx;
                pDockItem = pDockItem_loopVariable;
                pDockItem.Width = pZoom;
                pDockItem.Height = pZoom;
                //向主窗体加载按钮图片
                pDockItem.Paint(DstGraphics, DockSettings, pDockItem.Argument);
                pY = pY + pDockItem.Width + 10;
                //加小三角
                if (pDockItem.LockItem && !pDockItem.StartIn.Trim().ToLower().Contains("info"))
                {
                    DstGraphics.DrawImage((Image)Properties.Resources.三角指向_左, pDockItem.X - 8, pDockItem.Y + pZoom / 2 - 9, 16, 18);
                }
            }
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="DstGraphics"></param>
        /// <param name="DockSettings"></param>
        /// <param name="CurX"></param>
        /// <param name="CurY"></param>
        public void Paint(Graphics DstGraphics, Settings DockSettings, float CurX = 0, float CurY = 0, bool isStop = false)
        {
            //2013-12-06 庞少军 修改成靠右
            IDockItem pDockItem = null;
            int pZoom = 0;
            //int pX = 0;
            //int pMax = 0;
            //pX = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
            //pMax = (int)(DockSettings.Icons.ZoomPx * DockSettings.Icons.ZoomWidth) / 2;
            int pY = 0;
            int pMaxY = 0;
            pY = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterTopMargin;
            pMaxY = (int)(DockSettings.Icons.ZoomPx * DockSettings.Icons.ZoomWidth) / 2;
            foreach (IDockItem pDockItem_loopVariable in m_DockItems)
            {
                pDockItem = pDockItem_loopVariable;
                //pDockItem.Y = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterTopMargin;
                //pDockItem.X = pX;
                //pZoom = (int)DockSettings.Icons.SizePx;
                //pZoom = (int)(pMax - Math.Abs(CurX - pDockItem.CentreX));
                //if (pZoom > pMax)
                //{
                //    pZoom = pMax;
                //}
                pDockItem.X = (int)DockSettings.Style.SkinDockInfo.BackGround.OuterLeftMargin;
                pDockItem.Y = pY;
                pZoom = (int)DockSettings.Icons.SizePx;
                //如果按钮不是CS或BS程序，不需要变化大小
                if (pDockItem.StartIn.Trim().ToLower() == "cs" || pDockItem.StartIn.Trim().ToLower() == "bs")
                {
                    //计算鼠标离图标中心点距离，离得越近，图标越小，离得越远，图标越大
                    pZoom = (int)(pMaxY - Math.Abs(CurY - pDockItem.CentreY));
                    if (pZoom > pMaxY)
                    {
                        pZoom = pMaxY;
                    }

                    if (pZoom < DockSettings.Icons.SizePx)
                    {
                        pZoom = (int)DockSettings.Icons.SizePx;
                    }
                }

                pDockItem.Width = pZoom;
                pDockItem.Height = pZoom;

                //pDockItem.X = pDockItem.RightX - pZoom;
                //向主窗体加载按钮图片
                pDockItem.Paint(DstGraphics, DockSettings);
                //pX = pX + pDockItem.Width + 10;
                pY = pY + pDockItem.Width + 10;

                //绘制提示框
                float stringWidth = 0;
                string tip = pDockItem.Argument;
                if (string.IsNullOrEmpty(tip))
                {
                    tip = pDockItem.Name;
                }
                bool isHaveNoSysSub=!FormatXml.isHaveNoSysSub(pDockItem.ID);
                if (!string.IsNullOrEmpty(tip))
                {
                    Font drawFont = new Font("Arial", 10);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    int space = (int)(pMaxY - Math.Abs(CurY - pDockItem.CentreY));
                    if (space > 72)
                    {

                        if (isHaveNoSysSub)
                        {
                            if (PublicVariableModel.frmSub != null)
                            {
                                PublicVariableModel.frmSub.Close();
                                PublicVariableModel.frmSub = null;
                                if (PublicVariableModel.isMouseMoveSubFrm)
                                {
                                    PublicVariableModel.isMouseMoveSubFrm = false;
                                }
                            }
                            SizeF drawSize = DstGraphics.MeasureString(tip, drawFont);
                            stringWidth = drawSize.Width;
                            DstGraphics.DrawImage((Image)Properties.Resources.框左__左, pDockItem.X + pDockItem.Width, pDockItem.Y + pZoom / 2 - 14, 8, 28);
                            DstGraphics.DrawImage((Image)Properties.Resources.框中, pDockItem.X + pDockItem.Width + 8, pDockItem.Y + pZoom / 2 - 14, stringWidth, 28);
                            DstGraphics.DrawImage((Image)Properties.Resources.框右__左, pDockItem.X + pDockItem.Width + stringWidth - tip.Length, pDockItem.Y + pZoom / 2 - 14, 18, 28);
                            DstGraphics.DrawString(tip, drawFont, drawBrush, pDockItem.X + pDockItem.Width + 8, pDockItem.Y + pZoom / 2 - 9);
                        }
                        else
                        {
                            if (PublicVariableModel.frmSub == null)
                            {
                                PublicVariableModel.frmSub = new FrmSubSystem(pDockItem.X + pDockItem.Width, pDockItem.Y + pZoom / 2 - 40, tip, pDockItem.ID);
                                PublicVariableModel.frmSub.Show();
                                if (PublicVariableModel.isMouseMoveSubFrm == false)
                                {
                                    PublicVariableModel.isMouseMoveSubFrm = true;
                                }
                            }
                        }
                    }
                }
                //加小三角
                if (pDockItem.LockItem && !pDockItem.StartIn.Trim().ToLower().Contains("info") && isHaveNoSysSub)
                {
                    DstGraphics.DrawImage((Image)Properties.Resources.三角指向_左, pDockItem.X - 8, pDockItem.Y + pZoom / 2 - 9, 16, 18);
                }
            }
            int height_Screen = Screen.PrimaryScreen.WorkingArea.Height;
            string strHosptailName = ConfigurationManager.ConnectionStrings["HosptailName"].ConnectionString;
            if (strHosptailName != "xy215")
            {
                DstGraphics.DrawImage((Image)Properties.Resources.关闭, 45, height_Screen - 40, Resources.关闭.Width - 10, Resources.关闭.Height - 10);
                DstGraphics.DrawImage((Image)Properties.Resources.人员信息, 0, height_Screen - 40, Resources.人员信息.Width - 10, Resources.人员信息.Height - 10);
                if (isStop)
                {
                    DstGraphics.DrawImage((Image)Properties.Resources.锁定图标点击状态, 0, height_Screen - 70, Resources.锁定图标点击状态.Width - 10, Resources.锁定图标点击状态.Height - 10);
                }
                else
                {
                    DstGraphics.DrawImage((Image)Properties.Resources.锁定图标常态状态, 0, height_Screen - 70, Resources.锁定图标常态状态.Width - 10, Resources.锁定图标常态状态.Height - 10);
                }
            }
            else
            {
                //2014-08-18 陈宝栋修改将人员信息按钮的按钮名称更换
                //DstGraphics.DrawImage((Image)Properties.Resources.关闭_咸阳, 45, height_Screen - 40, Resources.关闭.Width - 10, Resources.关闭.Height - 10);
                //DstGraphics.DrawImage((Image)Properties.Resources.最小化, 0, height_Screen - 40, Resources.人员信息.Width - 10, Resources.人员信息.Height - 10);
                DstGraphics.DrawImage((Image)Properties.Resources.单点关闭点击态, 54, height_Screen - 40, Resources.单点关闭点击态.Width - 10, Resources.单点关闭点击态.Height - 10);
                DstGraphics.DrawImage((Image)Properties.Resources.隐藏点击态, 0, height_Screen - 40, Resources.隐藏点击态.Width - 10, Resources.隐藏点击态.Height - 10);
                Font fontUseInfo = new Font("微软雅黑", 10, FontStyle.Bold);
                SolidBrush drawBrushUserInfo = new SolidBrush(Color.White);
                string strUserName = PublicVariableModel.userInfoModelList.UserInfoList[0].Name;
                //Bitmap bitMapImage = new Bitmap((Image)Properties.Resources.人员信息_咸阳);
                //Bitmap bitMapImage = new Bitmap((Image)Properties.Resources.个人信息常态);
                SizeF userNameSize = DstGraphics.MeasureString(strUserName, fontUseInfo);
                //DstGraphics.DrawImage(bitMapImage, 0, height_Screen - 70, 77, Resources.人员信息.Height - 5);
                //DstGraphics.DrawImage(bitMapImage, 0, height_Screen - 70, 77, Resources.个人信息常态.Height - 5);
                //DstGraphics.DrawImage((Image)Properties.Resources.个人信息常态, 27, height_Screen - 40, Resources.个人信息常态.Width - 10, Resources.个人信息常态.Height - 10);
                DstGraphics.DrawString(strUserName, fontUseInfo, drawBrushUserInfo, (float)(38.5 - userNameSize.Width / 2), height_Screen - 67);
            }
            if (PublicVariableModel.isOnePageOrOthers > PublicVariableModel.IcoinCount)
            {
                double page = PublicVariableModel.isOnePageOrOthers / (float)PublicVariableModel.IcoinCount;
                double totalPage = Math.Ceiling(page);
                if (PublicVariableModel.pageCount != 0)
                {
                    DstGraphics.DrawImage((Image)Resources.翻页键hover3, 0, height_Screen - 90, Resources.翻页键hover3.Width, Resources.翻页键hover3.Height);
                }
                if (PublicVariableModel.pageCount != (totalPage - 1))
                {
                    DstGraphics.DrawImage((Image)Resources.翻页键hover4, 50, height_Screen - 90, Resources.翻页键hover4.Width, Resources.翻页键hover4.Height);
                }
            }
        }
    }
}
