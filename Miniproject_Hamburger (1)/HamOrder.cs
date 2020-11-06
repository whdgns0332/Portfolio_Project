using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//시간되면 DB에서 메뉴에 따라 생성자 생성되게 고쳐야함.


namespace Miniproject_Hamburger
{ 
    public class HamOrder
    {
        Dictionary<int, int>orderspec = new Dictionary<int, int>();
        public List<int> orderList;
        int TotalAmount;
        int orderNum;
        int orderCount;
        bool[] machineCheck = new bool[3] { false, false, false };
        /*
         * 1 빅맥       5000원   60초
         * 2 불고기버거 3500원   40초
         * 3 치즈버거   4000원   50초
         * 4 맥모닝     2500원   30초
         */
        public bool OrderEndCheck()
        {
            if (TotalAmount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void MachineOn(int machineNum)
        {
            machineCheck[machineNum] = true;
        }
        public void MachineOff(int machineNum)
        {
            machineCheck[machineNum] = false;
        }
        public bool MachineCheckingOn()
        {
            int count = 0;
            for(int i=0; i < 3; i++)
            {
                if(machineCheck[i] == true)
                {
                    count++;
                }
            }
            if (count == 0) return true;
            else return false;
        }
        public int OrderNumCheck()
        {
            return this.orderNum;
        }
        public HamOrder()
        {
            orderList = new List<int>();
        }

        public HamOrder(int orderNum) : this()
        {
            this.orderNum = orderNum;
            AddOrder(orderNum);
        }

        public void AddOrder(int orderNum)
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT MenuID, Amount " +
                                  "  FROM dbo.orderdetail " +
                                  " WHERE ID = @ID";

                SqlParameter parmID = new SqlParameter("@ID", SqlDbType.Int);
                parmID.Value = orderNum;
                cmd.Parameters.Add(parmID);

                SqlDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    this.orderspec.Add(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()));
                }
                orderCount = orderspec.Count();
            }

            for (int i = 1; i <= orderCount; i++)
            {
                if(orderspec.ContainsKey(i) == false)
                {
                    orderCount++;
                    continue;
                }
                for(int r=0; r<orderspec[i]; r++)
                {
                    orderList.Add(i); //  ID값넣음
                    TotalAmount++;
                }
            }
        }
        public void TotalMinus()
        {
            TotalAmount = TotalAmount - 1;
        }
    }

}
