using Ganss.Excel;
using Shared.Core;
using Shared.Model;

namespace JsonConverter
{
    public partial class JsonConverter : Form
    {
        string ClientOutPutDirectory = "..\\..\\Client\\Assets\\Resources\\JsonData";
        string ServerOutPutDirectory = "..\\..\\Server\\JsonData";

        List<Type> TypeList = new List<Type>();

        public JsonConverter()
        {
            InitializeComponent();

            TypeList.AddRange(typeof(JsonModel).Assembly.GetTypes().Where(v => v.IsSubclassOf(typeof(JsonModel))));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResultTextBox.Text = "...";
        }

        private void ExcelLoadTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (!excelFilePaths.Any()) { return; }

            try
            {
                foreach (var filePath in excelFilePaths)
                {
                    var files = filePath.Split('\\');
                    var fileName = files[files.Length - 1].Split('.')[0];

                    ResultTextBox.Text = $"Start parsing {fileName}.";

                    var type = TypeList.FirstOrDefault(v => v.Name == fileName);
                    
                    var data = new ExcelMapper(filePath).Fetch(type);
                    var result = JsonService.SerializePlainObject(data);

                    CreateFile(ClientOutPutDirectory, fileName, result);
                    CreateFile(ServerOutPutDirectory, fileName, result);

                    ResultTextBox.Text = $"{fileName} parse done.";
                }

                ResultTextBox.Text = $"parse done.";
            }
            catch (Exception exception)
            {
                ResultTextBox.Text = $"error: {exception.Message}";
                throw;
            }
        }

        private void ExcelLoadButton_Click(object sender, EventArgs e)
        {
            var openFilePath = System.Environment.CurrentDirectory;
            var openFiles = new List<string>();

            excelFilePaths.Clear();
            ExcelLoadTextBox.Clear();

            ExcelOpenFileDialog.InitialDirectory = openFilePath;         //�ʱ���
            ExcelOpenFileDialog.RestoreDirectory = true;                 //���� ��ΰ� ���� ��η� �����Ǵ��� ����          

            ExcelOpenFileDialog.Title = "���� ����";

            ExcelOpenFileDialog.DefaultExt = "*";

            ExcelOpenFileDialog.FileName = ""; //�⺻�� ���ϸ�

            ExcelOpenFileDialog.Multiselect = true;                      //�������ϼ���
            ExcelOpenFileDialog.ReadOnlyChecked = true;                  //�б��������� ��������üũ
            ExcelOpenFileDialog.ShowReadOnly = true;                     //�б��������� ���̱� 

            DialogResult dr = ExcelOpenFileDialog.ShowDialog();
            
            if (dr == DialogResult.OK)
            {
                foreach (var filePath in ExcelOpenFileDialog.FileNames)
                {
                    var files = filePath.Split('\\');
                    var fileName = files[files.Length - 1];

                    openFiles.Add(fileName);
                    excelFilePaths.Add(filePath);
                }
            }

            ExcelLoadTextBox.Text = String.Join("\r\n", openFiles);

            return;
        }

        private void CreateFile(string directory, string fileName, string result)
        {
            if (!directory.EndsWith("/"))
                directory += "/";
            Directory.CreateDirectory(directory);

            StreamWriter strmWriter = new StreamWriter(directory + fileName + ".json", false, System.Text.Encoding.UTF8);
            strmWriter.Write(result);
            strmWriter.Close();
        }

        public static T GetValue<T>(String value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}