using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace AdminSettings
{
    public partial class Form1 : Form
    {
        //****************Class Vars*********************************************        
        private List<OutCome> outcomeList_slotMachine_1 = new List<OutCome> { };
        private List<OutCome> outcomeList_slotMachine_2 = new List<OutCome> { };
        private List<OutCome> outcomeList_slotMachine_3 = new List<OutCome> { };
        //add more json lists here        
        // use this list for deafult values for a slotMachine        
        private List<OutCome> outcomeList_slotMachine_default = new List<OutCome> { OutCome.W, OutCome.W, OutCome.L, OutCome.NM, OutCome.L };
        enum Settings
        {
            SlotMachine_1,
            SlotMachine_2,
            SlotMachine_3,
            Environment_Calm,
            Environment_Chaos
        }

        enum OutCome
        {
            W,
            L,
            NM
        }

        class SlotMachineObj
        {
            public List<String> OutcomeList { get; set; }
        }

        //****************Constructor********************************************
        public Form1()
        {
            InitializeComponent();
            settings_comboBox.Items.Add(Settings.SlotMachine_1);
            settings_comboBox.Items.Add(Settings.SlotMachine_2);
            settings_comboBox.Items.Add(Settings.SlotMachine_3);
            // do not show environ stuff for now
            /*settings_comboBox.Items.Add(Settings.Environment_Calm);
            settings_comboBox.Items.Add(Settings.Environment_Chaos);*/
            overwriteOutComeList(outcomeList_slotMachine_1, outcomeList_slotMachine_default);
            overwriteOutComeList(outcomeList_slotMachine_2, outcomeList_slotMachine_default);
            overwriteOutComeList(outcomeList_slotMachine_3, outcomeList_slotMachine_default);
        }

        //****************Helpers Functions***********************************

        // function used as delegate for converting all items in outcome list to string
        private String outComeToString(OutCome outcome)
        {
            return outcome.ToString();
        }

        // overwrites a outcome list with another
        private void overwriteOutComeList(List<OutCome> listToOverwrite, List<OutCome> overwireFromList)
        {
            Console.WriteLine("Overwriting list");
            listToOverwrite.Clear();
            foreach (OutCome outcome in overwireFromList)
            {
                listToOverwrite.Add(outcome);
            }
        }
        //sets a listBox to an outcomeList
        //setListBoxToOutcomeList(ListBox listBox, List<OutCome> outcomeList)
        private void setListBoxToOutcomeList(List<OutCome> outcomeList)
        {
            listBox_slotMach_1.BeginUpdate();
            listBox_slotMach_1.Items.Clear(); //delete listBox items 
            foreach (OutCome outCome in outcomeList) //update listBox with new items
            {
                listBox_slotMach_1.Items.Add(outCome);
            }
            listBox_slotMach_1.EndUpdate();
        }

        private void addWinBtnAction(ListBox listBoxList)
        {
            Console.WriteLine("Running helper func for AddWin");
            listBoxList.BeginUpdate();
            listBoxList.Items.Add(OutCome.W);
            listBoxList.EndUpdate();
        }
        private void addNearMissBtnAction(ListBox listBoxList)
        {
            Console.WriteLine("Running helper func for AddNearMiss");
            listBoxList.BeginUpdate();
            listBoxList.Items.Add(OutCome.NM);
            listBoxList.EndUpdate();
        }

        private void addLossBtnAction(ListBox listBoxList)
        {
            Console.WriteLine("Running helper func for AddLoss");
            listBoxList.BeginUpdate();
            listBoxList.Items.Add(OutCome.L);
            listBoxList.EndUpdate();
        }

        private void writeJsonToFile(Object settings)
        {
            string exePath = Application.StartupPath + "";
            string fileName = "";
            SlotMachineObj slotObj;

            switch (settings)
            {
                case Settings.SlotMachine_1:
                    List<String> jsonOutputString_slotMachine1 = outcomeList_slotMachine_1.ConvertAll(new Converter<OutCome, string>(outComeToString));
                    slotObj = new SlotMachineObj() { OutcomeList = jsonOutputString_slotMachine1 };
                    fileName = Settings.SlotMachine_1.ToString();
                    break;
                case Settings.SlotMachine_2:
                    List<String> jsonOutputString_slotMachine2= outcomeList_slotMachine_2.ConvertAll(new Converter<OutCome, string>(outComeToString));
                    slotObj = new SlotMachineObj() { OutcomeList = jsonOutputString_slotMachine2};
                    fileName = Settings.SlotMachine_2.ToString();
                    break;
                case Settings.SlotMachine_3:
                    List<String> jsonOutputString_slotMachine3 = outcomeList_slotMachine_3.ConvertAll(new Converter<OutCome, string>(outComeToString));
                    slotObj = new SlotMachineObj() { OutcomeList = jsonOutputString_slotMachine3 };
                    fileName = Settings.SlotMachine_3.ToString();
                    break;
                default:
                    List<String> jsonOutputString_default = outcomeList_slotMachine_default.ConvertAll(new Converter<OutCome, string>(outComeToString));
                    slotObj = new SlotMachineObj() { OutcomeList = jsonOutputString_default };
                    fileName = "slotmachine_default_case";
                    break;

            }
            TextWriter txt = new StreamWriter(exePath + "\\" + fileName + ".json");
            txt.Write(JsonSerializer.Serialize(slotObj));
            txt.Close();
        }


        //****************Gui Action Functions***********************************
        //settings_comboBox (DropDown)
        private void settings_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    Console.WriteLine("case settings: " + Settings.SlotMachine_1);
                    groupBox_slotMach_1.Visible = true;
                    groupBox_Envr.Visible = false;
                    setListBoxToOutcomeList(outcomeList_slotMachine_1);
                    listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count; // to show current list size
                    break;
                case Settings.SlotMachine_2:
                    Console.WriteLine("case settings: " + Settings.SlotMachine_2);
                    groupBox_slotMach_1.Visible = true;
                    groupBox_Envr.Visible = false;
                    setListBoxToOutcomeList(outcomeList_slotMachine_2);
                    listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
                    break;
                case Settings.SlotMachine_3:
                    Console.WriteLine("case settings slot machine1: " + Settings.SlotMachine_3);
                    groupBox_slotMach_1.Visible = true;
                    groupBox_Envr.Visible = false;
                    setListBoxToOutcomeList(outcomeList_slotMachine_3);
                    listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
                    break;
                case Settings.Environment_Calm:
                    Console.WriteLine("case settings: " + Settings.Environment_Calm);
                    groupBox_slotMach_1.Visible = false;
                    groupBox_Envr.Visible = true;
                    listSizeNum_Envr_label.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
                    break;
                case Settings.Environment_Chaos:
                    Console.WriteLine("case settings: " + Settings.Environment_Chaos);
                    groupBox_slotMach_1.Visible = false;
                    groupBox_Envr.Visible = true;
                    listSizeNum_Envr_label.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
                    break;
                default:
                    Console.WriteLine("Default case for comboBox");
                    groupBox_slotMach_1.Visible = false;
                    groupBox_Envr.Visible = false;
                    break;
            }

            // attach list box to selected object's list
            /*List<int> slotMachinesIndexes = new List<int> { 0, 1, 2 };
            if (slotMachinesIndexes.Contains(settings_comboBox.SelectedIndex))
            {
                groupBox_slotMach_1.Visible = true;
                groupBox_Envr.Visible = false;
                listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
            }
            //envir objs
            else if (settings_comboBox.SelectedIndex == 3 || settings_comboBox.SelectedIndex == 4)
            {
                groupBox_slotMach_1.Visible = false;
                groupBox_Envr.Visible = true;
                //listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count; // to show default list size
            }
            else
            {
                groupBox_slotMach_1.Visible = false;
                groupBox_Envr.Visible = false;
            }*/
        }

        //_slotMach_1
        private void addWin_btn_slotMach_1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + addWin_btn_slotMach_1.Name);
            /*addWinBtnAction(listBox_slotMach_1);
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;*/
            //outcomeList_slotMachine_1.Clear();

            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    outcomeList_slotMachine_1.Add(OutCome.W);
                    setListBoxToOutcomeList(outcomeList_slotMachine_1);
                    break;
                case Settings.SlotMachine_2:
                    outcomeList_slotMachine_2.Add(OutCome.W);
                    setListBoxToOutcomeList(outcomeList_slotMachine_2);
                    break;
                case Settings.SlotMachine_3:
                    outcomeList_slotMachine_3.Add(OutCome.W);
                    setListBoxToOutcomeList(outcomeList_slotMachine_3);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but W button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
            Console.WriteLine(outcomeList_slotMachine_1.Count);
        }

        //_slotMach_1
        private void addNearMiss_btn_slotMach_1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + addNearMiss_btn_slotMach_1.Name);
            // addNearMissBtnAction(listBox_slotMach_1);


            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    outcomeList_slotMachine_1.Add(OutCome.NM);
                    setListBoxToOutcomeList(outcomeList_slotMachine_1);
                    Console.WriteLine("outcomelist1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    outcomeList_slotMachine_2.Add(OutCome.NM);
                    setListBoxToOutcomeList(outcomeList_slotMachine_2);
                    Console.WriteLine("outcomelist2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    outcomeList_slotMachine_3.Add(OutCome.NM);
                    setListBoxToOutcomeList(outcomeList_slotMachine_3);
                    Console.WriteLine("outcomelist3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but NM button clicked");
                    break;
            }
            //Console.WriteLine(outcomeList_slotMachine_1.Count);
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
        }

        //_slotMach_1
        private void addLoss_btn_slotMach_1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + addLoss_btn_slotMach_1.Name);
            //addLossBtnAction(listBox_slotMach_1);            

            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    outcomeList_slotMachine_1.Add(OutCome.L);
                    setListBoxToOutcomeList(outcomeList_slotMachine_1);
                    Console.WriteLine("outcomelist1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    outcomeList_slotMachine_2.Add(OutCome.L);
                    setListBoxToOutcomeList(outcomeList_slotMachine_2);
                    Console.WriteLine("outcomelist2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    outcomeList_slotMachine_3.Add(OutCome.L);
                    setListBoxToOutcomeList(outcomeList_slotMachine_3);
                    Console.WriteLine("outcomelist3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but L button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
            //nsole.WriteLine(outcomeList_slotMachine_1.Count);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void clearJson_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + clearJson_btn.Name);
            Console.WriteLine("clear list box");
            listBox_slotMach_1.BeginUpdate();
            listBox_slotMach_1.Items.Clear();
            listBox_slotMach_1.EndUpdate();

            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    outcomeList_slotMachine_1.Clear();
                    Console.WriteLine("outcome list1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    outcomeList_slotMachine_2.Clear();
                    Console.WriteLine("outcome list2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    outcomeList_slotMachine_3.Clear();
                    Console.WriteLine("outcome list3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but ClearJson button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
        }

        private void restoreAllDefaults_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + restoreAllDefaults_btn.Name);
            Console.WriteLine("Clear and then Restore default json values");

            //set defaults for all slot machines
            setListBoxToOutcomeList(outcomeList_slotMachine_default);
            overwriteOutComeList(outcomeList_slotMachine_1, outcomeList_slotMachine_default);
            overwriteOutComeList(outcomeList_slotMachine_2, outcomeList_slotMachine_default);
            overwriteOutComeList(outcomeList_slotMachine_3, outcomeList_slotMachine_default);

            //set defaults for all environs

            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void delSelectedItem_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + delSelectedItem_btn_slotMach_1.Name);
            if (listBox_slotMach_1.SelectedIndex < 0)
            {
                Console.WriteLine("No List box option selected to remove");
            }
            else
            {
                int indexToRemove = listBox_slotMach_1.SelectedIndex;
                Console.WriteLine("Deleting selected item: " + listBox_slotMach_1.SelectedItem);

                //listBox_slotMach_1.Items.RemoveAt(listBox_slotMach_1.SelectedIndex);
                //listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;

                Object settings = settings_comboBox.SelectedItem;
                switch (settings)
                {
                    case Settings.SlotMachine_1:
                        outcomeList_slotMachine_1.RemoveAt(indexToRemove);
                        setListBoxToOutcomeList(outcomeList_slotMachine_1);
                        Console.WriteLine("outcome list1 size: " + outcomeList_slotMachine_1.Count);
                        break;
                    case Settings.SlotMachine_2:
                        outcomeList_slotMachine_2.RemoveAt(indexToRemove);
                        setListBoxToOutcomeList(outcomeList_slotMachine_2);
                        Console.WriteLine("outcome list2 size: " + outcomeList_slotMachine_2.Count);
                        break;
                    case Settings.SlotMachine_3:
                        outcomeList_slotMachine_3.RemoveAt(indexToRemove);
                        setListBoxToOutcomeList(outcomeList_slotMachine_3);
                        Console.WriteLine("outcome list3 size: " + outcomeList_slotMachine_3.Count);
                        break;
                    default:
                        Console.WriteLine("No slot machine selected  but Random button clicked");
                        break;
                }
                listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
            }
        }

        private void randomizeItems_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + randomizeItems_btn_slotMach_1.Name);

            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    outcomeListRandomizer(outcomeList_slotMachine_1);
                    setListBoxToOutcomeList(outcomeList_slotMachine_1);
                    Console.WriteLine("outcome list1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    outcomeListRandomizer(outcomeList_slotMachine_2);
                    setListBoxToOutcomeList(outcomeList_slotMachine_2);
                    Console.WriteLine("outcome list2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    outcomeListRandomizer(outcomeList_slotMachine_3);
                    setListBoxToOutcomeList(outcomeList_slotMachine_3);
                    Console.WriteLine("outcome list3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but Random button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;

        }

        private void outcomeListRandomizer(List<OutCome> listToRandomize)
        {
            Random r = new Random();
            //  Randomize items in list
            //  http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
            Console.WriteLine("Randomizing list");
            for (int n = listToRandomize.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                OutCome temp = listToRandomize[n];
                listToRandomize[n] = listToRandomize[k];
                listToRandomize[k] = temp;
            }
        }

        private void saveAll_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + saveAll_btn.Name);
            //List<String> jsonOutputStringList = new List<String> { };                                 
            //save json for each file           
            //writeJsonToFile(settings_comboBox.SelectedItem);
            foreach (Settings setting in settings_comboBox.Items)
            {
                writeJsonToFile(setting);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + save_btn.Name);
            writeJsonToFile(settings_comboBox.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void restoreDefaults_slotMac_btn_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + restoreDefaults_slotMac_btn.Name);
            setListBoxToOutcomeList(outcomeList_slotMachine_default);
            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    overwriteOutComeList(outcomeList_slotMachine_1, outcomeList_slotMachine_default);
                    Console.WriteLine("outcome list1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    overwriteOutComeList(outcomeList_slotMachine_2, outcomeList_slotMachine_default);
                    Console.WriteLine("outcome list2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    overwriteOutComeList(outcomeList_slotMachine_3, outcomeList_slotMachine_default);
                    Console.WriteLine("outcome list3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but Random button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void restoreDefaults_btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked: " + restoreDefaults_btn.Name);
            /*Console.WriteLine("clicked: " + restoreDefaults_btn.Name);
            setListBoxToOutcomeList(outcomeList_slotMachine_default);
            Object settings = settings_comboBox.SelectedItem;
            switch (settings)
            {
                case Settings.SlotMachine_1:
                    overwriteOutComeList(outcomeList_slotMachine_1, outcomeList_slotMachine_default);                    
                    Console.WriteLine("outcome list1 size: " + outcomeList_slotMachine_1.Count);
                    break;
                case Settings.SlotMachine_2:
                    overwriteOutComeList(outcomeList_slotMachine_2, outcomeList_slotMachine_default);
                    Console.WriteLine("outcome list2 size: " + outcomeList_slotMachine_2.Count);
                    break;
                case Settings.SlotMachine_3:
                    overwriteOutComeList(outcomeList_slotMachine_3, outcomeList_slotMachine_default);
                    Console.WriteLine("outcome list3 size: " + outcomeList_slotMachine_3.Count);
                    break;
                default:
                    Console.WriteLine("No slot machine selected  but Random button clicked");
                    break;
            }
            listSizeNum_label_slotMachine_1.Text = "" + listBox_slotMach_1.Items.Count;*/


        }
    }
}
