using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectGroup
{
    public partial class hoadon : Form
    {
        public hoadon()
        {
            InitializeComponent();
            HienthiGridviewDSTHUEPHONG();
            hienthigview2();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void btthoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
            // this.Close();
            qltraphong tp = new qltraphong();
            tp.Show();
            this.Hide();
        }


        private void bttk_Click(object sender, EventArgs e)
        {
        }
        public void HienthiGridviewDSTHUEPHONG()
        {
                DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                column1.DataPropertyName = "maPT";
                column1.HeaderText = "Mã PT";
                dtgview1.Columns.Add(column1);

                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                column2.DataPropertyName = "maNV";
                column2.HeaderText = "Mã NV";
                dtgview1.Columns.Add(column2);

                DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                column3.DataPropertyName = "maKH";
                column3.HeaderText = "Mã KH";
                dtgview1.Columns.Add(column3);

                DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
                column4.DataPropertyName = "ngayDen";
                column4.HeaderText = "Ngày đến";
                dtgview1.Columns.Add(column4);

                DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
                column5.DataPropertyName = "ngayDi";
                column5.HeaderText = "Ngày đi";
                dtgview1.Columns.Add(column5);

                DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
                column6.DataPropertyName = "soPhong";
                column6.HeaderText = "Số phòng";
                dtgview1.Columns.Add(column6);


        }

        private void dtgview1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtmpt.Text = dtgview1.CurrentRow.Cells[0].Value.ToString();
            txtmkh.Text = dtgview1.CurrentRow.Cells[2].Value.ToString();
            txtsp.Text = dtgview1.CurrentRow.Cells[5].Value.ToString();
            
            txtmnv.Text = dtgview1.CurrentRow.Cells[1].Value.ToString();
            //txtmp.Text = dtgview1.CurrentRow.Cells[4].Value.ToString();
            DataTable dt = new DataTable();
            App_Config cf = new App_Config();
            String sql = "SELECT maLP ";
            sql += " FROM Phong ";
            sql += " Where soPhong='" + txtsp.Text + "'";
            dt = cf.SelectDB(sql);
            txtmp.Text=dt.Rows[0][0].ToString();

            

            DataTable dt1 = new DataTable();
            String sql1 = " SELECT ngayDen From DSThuePhong where maPT='"+txtmpt.Text+"'";
            dt1 = cf.SelectDB(sql1);
            String ngayDen= dt1.Rows[0][0].ToString();


            DataTable dt2 = new DataTable();
            String sql2 = " SELECT DATEDIFF(day,convert(datetime ,'" + ngayDen + "',102), convert(datetime, '" + datetime.Value + "',102))";
            dt2 = cf.SelectDB(sql2);
            txtsno.Text = dt2.Rows[0][0].ToString();

            DataTable dt3 = new DataTable();
            String sql3 = "select gia from Phong ";
            sql3 += "inner join Loaiphong on Loaiphong.maLP=Phong.maLP ";
            sql3 += "where soPhong='" + txtsp.Text + "'";
            dt3 = cf.SelectDB(sql3);
            String giaPhong = dt3.Rows[0][0].ToString();

            int gia = Int32.Parse(giaPhong);
            int ngay = Int32.Parse(txtsno.Text);
            int tong = gia * ngay;
            txttt.Text = tong.ToString();

            //mã nhân viên thanh toán cần được lấy lại.
            txtmnv1.Text = txtmnv.Text;
        }
        public void hienthigview2()
        {
            
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "maHD";
                column.HeaderText = "Mã HD";
                dtgview2.Columns.Add(column);
         
                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                column2.DataPropertyName = "maPT";
                column2.HeaderText = "Mã PT";
                dtgview2.Columns.Add(column2);
                
                DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                column3.DataPropertyName = "tongTien";
                column3.HeaderText = "Tiền phòng";
                dtgview2.Columns.Add(column3);

                DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
                column4.DataPropertyName = "ngayTT";
                column4.HeaderText = "Ngày thanh toán";
                dtgview2.Columns.Add(column4);

                DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
                column5.DataPropertyName = "maNVTT";
                column5.HeaderText = "Mã Nhân viên Thanh toán";
                dtgview2.Columns.Add(column5);


        }

        private void dtgview2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtmhd_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtmhd_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btttoan_Click(object sender, EventArgs e)
        {
            int count = 0;
            count = dtgview2.Rows.Count;
            String ma1 = "HD";
            int ma2 = count;//-1=-1+1 trừ 1 hàng thừa cộng 1 hàng mới..
            txtmhd.Text = ma1 + ma2.ToString();
            App_Config configDB = new App_Config();

                String sqlThemDuLieu = "INSERT INTO Hoadon (maHD , maPT, tongtien, ngayTT, maNVTT)";
                sqlThemDuLieu += "VALUES('" + txtmhd.Text + "',N'" + txtmpt.Text + "'";
                sqlThemDuLieu += ",'" + txttt.Text + "',N'" + datetime.Value + "', '" + txtmnv1.Text + "');";
                configDB.InsertDB(sqlThemDuLieu);

                String sqlUpdate = "UPDATE DSThuePhong SET ngayDi='" + datetime.Value + "'";
                sqlUpdate += " WHERE maPT='" + txtmpt.Text + "'";
                configDB.InsertDB(sqlUpdate);

                String sqlUpdate1 = "UPDATE Phong SET maTT=1";
                sqlUpdate1 += " WHERE soPhong='" + txtsp.Text + "'";
                configDB.InsertDB(sqlUpdate1);

                hoadon_Load(sender, e);

            
        }

        private void hoadon_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            App_Config configdb = new App_Config();
            String sql = "SELECT maPT,maNV,maKH,convert(varchar,ngayDen,103) as ngayDen, convert(varchar,ngayDi,103) as ngayDi,DSThuePhong.soPhong";
            sql += " FROM DSThuePhong inner join Phong on Phong.soPhong=DSThuePhong.soPhong where maTT = 2  ";
            dt = configdb.SelectDB(sql);
            dtgview1.DataSource = dt;



            App_Config cf = new App_Config();
            DataTable dt1 = new DataTable();
            String sql1 = "select maHD , maPT, tongtien, convert(varchar,ngayTT,103) as ngayTT, maNVTT from Hoadon ";
            dt1 = cf.SelectDB(sql1);
            dtgview2.DataSource = dt1;
    }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtmkh_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
