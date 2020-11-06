using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace Miniproject_Hamburger
{
    public partial class OrderForm : Form
    {
        List<OrderItem> orderItems;

        public OrderForm()
        {
            InitializeComponent();
            orderItems = new List<OrderItem>();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            Panel_Menu_Initialize();
            ComboBox_Ingre_Initialize();

            Panel_Order.FlowDirection = FlowDirection.LeftToRight;
            Panel_Order.Dock = DockStyle.Fill;
        }

        //각각의 상품을 버튼으로서 동적으로 생성하는 함수
        private void Panel_Menu_Initialize()
        {
            Label_SelectedItem.Text = "";
            Panel_Menu.FlowDirection = FlowDirection.LeftToRight;
            Panel_Menu.Dock = DockStyle.Fill;
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT ID, Name from menuTBl"
                };

                SqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    Button button = new Button
                    {
                        Size = new Size(180, 100),
                        Font = new Font("MS UIGothic", 20),
                        Text = reader["Name"].ToString(),
                        Tag = int.Parse(reader["ID"].ToString())
                    };
                    button.Click += new EventHandler(MenuButton_Click);
                    Panel_Menu.Controls.Add(button);
                    if (count % 4 == 3)
                        Panel_Menu.SetFlowBreak(button, true);
                    count++;
                }

                connection.Close();
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string id = button.Tag.ToString();

            ComboBox_ItemSelect(int.Parse(id), button.Text, 1);
        }

        //재료 목록을 나타내는 ComboBox들을 초기화하는 함수
        private void ComboBox_Ingre_Initialize()
        {
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT ID, IngrType, Name from ingredientsTbl"
                };
                SqlDataReader reader = command.ExecuteReader();

                Dictionary<string, string> bun = new Dictionary<string, string>();
                Dictionary<string, string> patty = new Dictionary<string, string>();
                Dictionary<string, string> cheese = new Dictionary<string, string>();
                Dictionary<string, string> vege = new Dictionary<string, string>();

                while (reader.Read())
                {
                    if(reader[1].ToString() == "번")
                    {
                        bun.Add(reader[0].ToString(), reader[2].ToString());
                    }
                    else if(reader[1].ToString() == "패티")
                    {
                        patty.Add(reader[0].ToString(), reader[2].ToString());
                    }
                    else if (reader[1].ToString() == "치즈")
                    {
                        cheese.Add(reader[0].ToString(), reader[2].ToString());
                    }
                    else if (reader[1].ToString() == "야채")
                    {
                        vege.Add(reader[0].ToString(), reader[2].ToString());
                    }
                }

                ComboBox_Ingre_Bun.DataSource = new BindingSource(bun, null);
                ComboBox_Ingre_Bun.DisplayMember = "Value";
                ComboBox_Ingre_Bun.ValueMember = "Key";
                ComboBox_Ingre_Bun.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboBox_Ingre_Bun.SelectedIndexChanged += new EventHandler(ComboBox_Ingre_Bun_SelectedIndexChanged);

                ComboBox_Ingre_Patty.DataSource = new BindingSource(patty, null);
                ComboBox_Ingre_Patty.DisplayMember = "Value";
                ComboBox_Ingre_Patty.ValueMember = "Key";
                ComboBox_Ingre_Patty.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboBox_Ingre_Patty.SelectedIndexChanged += new EventHandler(ComboBox_Ingre_Patty_SelectedIndexChanged);

                ComboBox_Ingre_Cheese.DataSource = new BindingSource(cheese, null);
                ComboBox_Ingre_Cheese.DisplayMember = "Value";
                ComboBox_Ingre_Cheese.ValueMember = "Key";
                ComboBox_Ingre_Cheese.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboBox_Ingre_Cheese.SelectedIndexChanged += new EventHandler(ComboBox_Ingre_Cheese_SelectedIndexChanged);

                ComboBox_Ingre_Vege.DataSource = new BindingSource(vege, null);
                ComboBox_Ingre_Vege.DisplayMember = "Value";
                ComboBox_Ingre_Vege.ValueMember = "Key";
                ComboBox_Ingre_Vege.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboBox_Ingre_Vege.SelectedIndexChanged += new EventHandler(ComboBox_Ingre_Vege_SelectedIndexChanged);

                ComboBox_MakeBlank();

                connection.Close();
            }
        }

        //상품이 선택되었을 때 해당 상품의 정보를 화면에 나타내는 함수
        private void ComboBox_ItemSelect(int id, string itemName, int amount)
        {
            ComboBox_MakeBlank();
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT RECIPE.MenuID, RECIPE.IngredientsID,
                                           RECIPE.Consume, INGRE.IngrType, INGRE.Name, INGRE.Amount
                                      FROM recipeTbl RECIPE
                                         INNER JOIN ingredientsTbl INGRE
                                                 ON RECIPE.IngredientsID = INGRE.ID
                                         WHERE RECIPE.MenuID = @MenuID"
                };
                SqlParameter paramMenuID = new SqlParameter("@MenuID", SqlDbType.Int);
                paramMenuID.Value = id;
                command.Parameters.Add(paramMenuID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    switch (reader[3].ToString())
                    {
                        case "번":
                            Label_Count_Bun.Text = reader[2].ToString();
                            ComboBox_Ingre_Bun.SelectedIndex = ComboBox_Ingre_Bun.FindString(reader[4].ToString());
                            StockUpdate(ComboBox_Ingre_Bun.SelectedValue, Label_Stock_Bun);
                            break;
                        case "패티":
                            Label_Count_Patty.Text = reader[2].ToString();
                            ComboBox_Ingre_Patty.SelectedIndex = ComboBox_Ingre_Patty.FindString(reader[4].ToString());
                            StockUpdate(ComboBox_Ingre_Patty.SelectedValue, Label_Stock_Patty);
                            break;
                        case "치즈":
                            Label_Count_Cheese.Text = reader[2].ToString();
                            ComboBox_Ingre_Cheese.SelectedIndex = ComboBox_Ingre_Cheese.FindString(reader[4].ToString());
                            StockUpdate(ComboBox_Ingre_Cheese.SelectedValue, Label_Stock_Cheese);
                            break;
                        case "야채":
                            Label_Count_Vege.Text = reader[2].ToString();
                            ComboBox_Ingre_Vege.SelectedIndex = ComboBox_Ingre_Vege.FindString(reader[4].ToString());
                            StockUpdate(ComboBox_Ingre_Vege.SelectedValue, Label_Stock_Vege);
                            break;
                    }
                }
                TextBox_Amount.Text = amount.ToString();
                Label_SelectedItem.Text = itemName;
                Label_SelectedItem.Tag = id;

                Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Bun.Text), Label_Count_Bun);
                Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Patty.Text), Label_Count_Patty);
                Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Cheese.Text), Label_Count_Cheese);
                Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Vege.Text), Label_Count_Vege);

                connection.Close();
            }
        }

        private void ComboBox_MakeBlank()
        {
            ComboBox_Ingre_Bun.SelectedIndex = -1;
            ComboBox_Ingre_Patty.SelectedIndex = -1;
            ComboBox_Ingre_Cheese.SelectedIndex = -1;
            ComboBox_Ingre_Vege.SelectedIndex = -1;

            Label_Count_Bun.Text = "0";
            Label_Count_Patty.Text = "0";
            Label_Count_Cheese.Text = "0";
            Label_Count_Vege.Text = "0";
            Label_Stock_Bun.Text = "0";
            Label_Stock_Patty.Text = "0";
            Label_Stock_Cheese.Text = "0";
            Label_Stock_Vege.Text = "0";
        }

        //제품 수량을 적는 TextBox에 자연수만을 적을 수 있도록 보정하는 함수
        int AmountLog = 0;
        private void TextBox_Amount_TextChanged(object sender, EventArgs e)
        {
            int amount;
            if(int.TryParse(TextBox_Amount.Text, out amount))
            {
                if (amount > 0)
                    AmountLog = amount;
                else
                {
                    TextBox_Amount.Text = AmountLog.ToString();
                    amount = AmountLog;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(TextBox_Amount.Text))
                {
                    TextBox_Amount.Text = AmountLog.ToString();
                    amount = AmountLog;
                }
                else
                    AmountLog = amount = 1;
            }

            Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Bun.Text), Label_Count_Bun);
            Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Patty.Text), Label_Count_Patty);
            Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Cheese.Text), Label_Count_Cheese);
            Label_Count_ColorAdjust(amount, int.Parse(Label_Stock_Vege.Text), Label_Count_Vege);
        }

        //재료 사용량이 재고량을 넘는 경우 붉게 표시하는 함수
        private void Label_Count_ColorAdjust(int amount, int stock, Label countLabel)
        {
            int count;
            if (!int.TryParse(countLabel.Text.ToString(), out count))
                count = 1;
            countLabel.Text = (amount * count).ToString();

            if (amount * count > stock)
                countLabel.ForeColor = Color.Red;
            else
                countLabel.ForeColor = Color.Black;
        }

        private void ComboBox_Ingre_Bun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Ingre_Bun.SelectedIndex >= 0)
                StockUpdate(ComboBox_Ingre_Bun.SelectedValue, Label_Stock_Bun);
        }
        private void ComboBox_Ingre_Patty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Ingre_Patty.SelectedIndex >= 0)
                StockUpdate(ComboBox_Ingre_Patty.SelectedValue, Label_Stock_Patty);
        }
        private void ComboBox_Ingre_Cheese_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Ingre_Cheese.SelectedIndex >= 0)
                StockUpdate(ComboBox_Ingre_Cheese.SelectedValue, Label_Stock_Cheese);
        }
        private void ComboBox_Ingre_Vege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Ingre_Vege.SelectedIndex >= 0)
                StockUpdate(ComboBox_Ingre_Vege.SelectedValue, Label_Stock_Vege);
        }

        //특정 재료 타입에 대해 재고량을 갱신하는 함수
        private void StockUpdate(object idValue, Label stockLabel)
        {
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT Amount from ingredientsTbl
                                     WHERE ID = @ID"
                };
                SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                paramID.Value = idValue;
                command.Parameters.Add(paramID);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                    stockLabel.Text = reader[0].ToString();
                connection.Close();
            }
        }

        private void Button_AddItem_Click(object sender, EventArgs e)
        {
            if(Label_SelectedItem.Tag != null)
            {
                if (Label_Count_Bun.ForeColor == Color.Red ||
                    Label_Count_Patty.ForeColor == Color.Red ||
                    Label_Count_Cheese.ForeColor == Color.Red ||
                    Label_Count_Vege.ForeColor == Color.Red)
                {
                    MessageBox.Show("남은 재고를 넘어서는 분량의 주문은 불가능합니다");
                    TextBox_Amount.Text = "1";
                    return;
                }

                int itemIndex = FindItemFromOrder(int.Parse( Label_SelectedItem.Tag.ToString() ));
                if (itemIndex >= 0)
                {
                    bool canParse;
                    int parseResult;
                    canParse = int.TryParse(TextBox_Amount.Text, out parseResult);
                    if (canParse && parseResult > 0)
                        orderItems.ElementAt(itemIndex).amount.Text = TextBox_Amount.Text;
                    else
                        RemoveItemFromOrder(int.Parse( Label_SelectedItem.Tag.ToString() ));
                }
                else
                {
                    OrderItem item = new OrderItem();
                    item.menuID.Text = Label_SelectedItem.Tag.ToString();
                    item.menuName.Text = Label_SelectedItem.Text;
                    item.amount.Text = TextBox_Amount.Text;

                    item.menuID.Click += new EventHandler(ClickItemFromOrder_Label);
                    item.menuName.Click += new EventHandler(ClickItemFromOrder_Label);
                    item.amount.Click += new EventHandler(ClickItemFromOrder_Label);
                    item.increase.Click += new EventHandler(IncreaseItemAmountFromOrder);
                    item.decrease.Click += new EventHandler(DecreaseItemAmountFromOrder);
                    Panel_Order.SetFlowBreak(item.decrease, true);

                    Panel_Order.Controls.Add(item.menuID);
                    Panel_Order.Controls.Add(item.menuName);
                    Panel_Order.Controls.Add(item.amount);
                    Panel_Order.Controls.Add(item.increase);
                    Panel_Order.Controls.Add(item.decrease);

                    orderItems.Add(item);
                }
            }
        }

        private void Button_RemoveItem_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Label_SelectedItem.Text))
            {
                int itemID = int.Parse(Label_SelectedItem.Tag.ToString());
                RemoveItemFromOrder(itemID);
            }
        }

        //주문 목록에서 특정 상품을 검색하는 함수
        //리턴값은 주문 목록 List에서 해당 상품의 index이며, 검색에 실패할 경우 -1을 리턴한다
        private int FindItemFromOrder(int itemID)
        {
            for(int i = 0; i < orderItems.Count; i++)
            {
                if (int.Parse(orderItems.ElementAt(i).menuID.Text) == itemID)
                    return i;
            }
            return -1;
        }

        //주문 목록에서 특정 상품을 제거하는 함수
        private void RemoveItemFromOrder(int itemID)
        {
            int itemLoc = FindItemFromOrder(itemID);
            if (itemLoc >= 0)
            {
                OrderItem item = orderItems.ElementAt(itemLoc);
                Panel_Order.Controls.Remove(item.menuID);
                Panel_Order.Controls.Remove(item.menuName);
                Panel_Order.Controls.Remove(item.amount);
                Panel_Order.Controls.Remove(item.increase);
                Panel_Order.Controls.Remove(item.decrease);
                orderItems.Remove(item);
            }
        }

        //주문 목록에서 상품 하나에 대한 Label을 눌렀을 경우의 행동을 나타내는 함수
        //상품 하나당 ID, 상품명, 상품 개수를 각각 나타내는 3개의 Label이 존재한다
        private void ClickItemFromOrder_Label(object sender, EventArgs e)
        {
            Label label = sender as Label;
            OrderItem item = label.Tag as OrderItem;
            ClickItemFromOrder(item);
        }

        //주문 목록에서 상품 하나에 대한 Button을 눌렀을 경우의 행동을 나타내는 함수
        //상품 하나당 개수 상승, 감소의 기능을 담당하는 Button 2개가 존재한다
        private void ClickItemFromOrder_Button(object sender, EventArgs e)
        {
            Button button = sender as Button;
            OrderItem item = button.Tag as OrderItem;
            ClickItemFromOrder(item);
        }

        //주문 목록에서 상품을 클릭했을 때의 공통 행동을 나타내는 함수
        private void ClickItemFromOrder(OrderItem item)
        {
            int id = int.Parse(item.menuID.Text);
            int amount = int.Parse(item.amount.Text);
            ComboBox_ItemSelect(id, item.menuName.Text, amount);
        }

        //주문 목록에서 한 상품의 개수를 1 증가시키는 함수
        private void IncreaseItemAmountFromOrder(object sender, EventArgs e)
        {
            Button button = sender as Button;
            OrderItem item = button.Tag as OrderItem;
            int amount = int.Parse(item.amount.Text) + 1;
            item.amount.Text = amount.ToString();

            ClickItemFromOrder_Button(sender, e);

            if (Label_Count_Bun.ForeColor == Color.Red ||
                    Label_Count_Patty.ForeColor == Color.Red ||
                    Label_Count_Cheese.ForeColor == Color.Red ||
                    Label_Count_Vege.ForeColor == Color.Red)
            {
                MessageBox.Show("남은 재고를 넘어서는 분량의 주문은 불가능합니다");
                DecreaseItemAmountFromOrder(sender, e);
            }
        }

        //주문 목록에서 한 상품의 개수를 1 감소시키는 함수
        private void DecreaseItemAmountFromOrder(object sender, EventArgs e)
        {
            Button button = sender as Button;
            OrderItem item = button.Tag as OrderItem;
            int amount = int.Parse(item.amount.Text) - 1;
            if(amount > 0)
                item.amount.Text = amount.ToString();
            else
                RemoveItemFromOrder(int.Parse(item.menuID.Text));

            ClickItemFromOrder_Button(sender, e);
        }


        private void Button_AddOrder_Click(object sender, EventArgs e)
        {
            MakeOrder();
        }

        //주문을 처리하는 함수
        private void MakeOrder()
        {
            if (orderItems.Count == 0)
                return;

            Dictionary<int, int> ingreComsumption = CountConsumption();
            if (!CheckStockForOrder(ingreComsumption))
                return;

            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlTransaction trans = connection.BeginTransaction();
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    Transaction = trans,
                };

                int orderID;
                try
                {
                    //orderTbl에 주문을 추가한다
                    command.CommandText = @"INSERT INTO orderTbl
                                                 VALUES (@orderDate)";
                    SqlParameter orderDate = new SqlParameter("@orderDate", SqlDbType.Date);
                    if (string.IsNullOrEmpty(TextBox_FakeMonth.Text))
                    {
                        orderDate.Value = DateTime.Today.Year + "-" +
                                          DateTime.Today.Month + "-" +
                                          DateTime.Today.Day;
                    }
                    else
                    {
                        orderDate.Value = DateTime.Today.Year + "-" +
                                      int.Parse(TextBox_FakeMonth.Text) + "-" +
                                      DateTime.Today.Day;
                    }
                    command.Parameters.Add(orderDate);
                    command.ExecuteNonQuery();
                    command.Parameters.Remove(orderDate);

                    //방금 추가한 주문의 주문ID를 가져온다
                    command.CommandText = "SELECT IDENT_CURRENT('orderTbl')";
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                        orderID = int.Parse(reader[0].ToString());
                    else
                        throw new Exception();
                    reader.Close();

                    //어떤 제품을 몇개 주문했는지를 DB에 저장한다
                    foreach(OrderItem item in orderItems)
                    {
                        command.CommandText = @"INSERT INTO orderdetail (ID, MenuID, Amount)
                                                     VALUES (@ID, @MenuID, @Amount)";
                        SqlParameter tblOrderID = new SqlParameter("@ID", SqlDbType.Int);
                                     tblOrderID.Value = orderID;
                        SqlParameter menuID = new SqlParameter("@MenuID", SqlDbType.Int);
                                     menuID.Value = int.Parse(item.menuID.Text);
                        SqlParameter amount = new SqlParameter("@Amount", SqlDbType.Int);
                                     amount.Value = int.Parse(item.amount.Text);

                        command.Parameters.Add(tblOrderID);
                        command.Parameters.Add(menuID);
                        command.Parameters.Add(amount);
                        command.ExecuteNonQuery();
                        command.Parameters.Remove(tblOrderID);
                        command.Parameters.Remove(menuID);
                        command.Parameters.Remove(amount);
                    }
                    
                    // 재료 사용량 만큼 재고를 감소시킨다
                    command.CommandText = "SELECT ID, Amount from ingredientsTbl";
                
                    reader = command.ExecuteReader();
                    Dictionary<int, int> stock = new Dictionary<int, int>();
                    List<int> ids = new List<int>();
                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        int amount = int.Parse(reader[1].ToString());
                        stock[id] = amount;
                        ids.Add(id);
                    }
                    reader.Close();

                    foreach(int id in ids)
                    {
                        if (ingreComsumption.ContainsKey(id))
                        {
                            int amount = stock[id] - ingreComsumption[id];
                            command.CommandText = @"UPDATE ingredientsTbl
                                                       SET Amount = @Amount
                                                     WHERE ID = @ID";
                            SqlParameter paramID = new SqlParameter("@ID", SqlDbType.Int);
                            paramID.Value = id;
                            SqlParameter paramAmount = new SqlParameter("@Amount", SqlDbType.Int);
                            paramAmount.Value = amount;
                            command.Parameters.Add(paramID);
                            command.Parameters.Add(paramAmount);
                            command.ExecuteNonQuery();
                            command.Parameters.Remove(paramID);
                            command.Parameters.Remove(paramAmount);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    trans.Rollback();
                    return;
                }

                for(int i = orderItems.Count - 1; i >= 0; i--)
                {
                    RemoveItemFromOrder(int.Parse(orderItems.ElementAt(i).menuID.Text));
                }

                //생산 대기 목록에 주문을 추가하기 위한 함수
                //구현은 내가 아닌 다른 조원이 함
                MainForm.newOrder(orderID);
                
                if(ComboBox_Ingre_Bun.SelectedValue != null)
                    StockUpdate(ComboBox_Ingre_Bun.SelectedValue, Label_Stock_Bun);
                if (ComboBox_Ingre_Patty.SelectedValue != null)
                    StockUpdate(ComboBox_Ingre_Patty.SelectedValue, Label_Stock_Patty);
                if (ComboBox_Ingre_Cheese.SelectedValue != null)
                    StockUpdate(ComboBox_Ingre_Cheese.SelectedValue, Label_Stock_Cheese);
                if (ComboBox_Ingre_Vege.SelectedValue != null)
                    StockUpdate(ComboBox_Ingre_Vege.SelectedValue, Label_Stock_Vege);

                MessageBox.Show("주문이 완료되었습니다", "주문 완료");
            }
        }

        //주문한 햄버거들이 소모하는 재료의 양을 리턴하는 함수
        private Dictionary<int, int> CountConsumption()
        {
            Dictionary<int, int> ingreComsumption = new Dictionary<int, int>();
            foreach (OrderItem item in orderItems)
            {
                using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = @"SELECT IngredientsID, Consume 
                                          FROM recipeTbl
                                         WHERE MenuID = @MenuID "
                    };
                    SqlParameter menuID = new SqlParameter("@MenuID", SqlDbType.Int);
                                 menuID.Value = int.Parse(item.menuID.Text);
                    command.Parameters.Add(menuID);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        int consume = int.Parse(reader[1].ToString());
                        int amount = int.Parse(item.amount.Text);
                        if (ingreComsumption.ContainsKey(id))
                            ingreComsumption[id] += consume * amount;
                        else
                            ingreComsumption[id] = consume * amount;
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return ingreComsumption;
        }

        //소모량과 재료의 재고를 비교해 재고가 충분한지 여부를 리턴하는 함수
        private bool CheckStockForOrder(Dictionary<int, int> ingreConsumption)
        {
            using (SqlConnection connection = new SqlConnection(Commons.CONSTRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = @"SELECT ID, Amount, Name
                                      FROM ingredientsTbl"
                };
                SqlDataReader reader = command.ExecuteReader();

                int id, stock;
                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                    stock = int.Parse(reader[1].ToString());

                    if (ingreConsumption.ContainsKey(id) && ingreConsumption[id] > stock)
                    {
                        MessageBox.Show($"{reader[2].ToString()}이(가) 부족합니다", "재고 부족");
                        reader.Close();
                        connection.Close();
                        return false;
                    }
                }
                reader.Close();
                connection.Close();
            }
            return true;
        }

    }

    //주문 내의 상품 목록을 다루기 위해
    //주문 내의 각 상품을 담당하는 클래스
    class OrderItem
    {
        public Label menuID;
        public Label menuName;
        public Label amount;
        public Button increase;
        public Button decrease;

        public OrderItem()
        {
            menuID = new Label();
            menuID.TextAlign = ContentAlignment.MiddleCenter;
            menuID.Size = new Size(100, 30);
            menuID.Tag = this;

            menuName = new Label();
            menuName.TextAlign = ContentAlignment.MiddleCenter;
            menuName.Size = new Size(400, 30);
            menuName.Tag = this;

            amount = new Label();
            amount.TextAlign = ContentAlignment.MiddleCenter;
            amount.Size = new Size(100, 30);
            amount.Tag = this;

            increase = new Button();
            increase.Size = new Size(50, 25);
            increase.BackColor = SystemColors.Control;
            increase.Text = "▲";
            increase.Tag = this;

            decrease = new Button();
            decrease.Size = new Size(50, 25);
            decrease.BackColor = SystemColors.Control;
            decrease.Text = "▼";
            decrease.Tag = this;
        }
    }
}