using Ganss.Excel;
using Shared.Core;
using Shared.Model;

namespace JsonConverter
{
    public partial class JsonConverter : Form
    {
        bool IsDebug = false; 
        string ClientOutPutDirectory = "..\\..\\Client\\Assets\\Resources\\JsonData";
        string ServerOutPutDirectory = "..\\..\\Server\\JsonData";

        string DebugOutPutDirectory = "..\\..\\..\\JsonData";

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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDebug = CheckBox.Checked;
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

                    if (IsDebug)
                    {
                        CreateFile(DebugOutPutDirectory, fileName, result);
                    }
                    else
                    {
                        CreateFile(ClientOutPutDirectory, fileName, result);
                        CreateFile(ServerOutPutDirectory, fileName, result);
                    }

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

            ExcelOpenFileDialog.InitialDirectory = openFilePath;         //초기경로
            ExcelOpenFileDialog.RestoreDirectory = true;                 //현재 경로가 이전 경로로 복원되는지 여부          

            ExcelOpenFileDialog.Title = "파일 선택";

            ExcelOpenFileDialog.DefaultExt = "*";

            ExcelOpenFileDialog.FileName = ""; //기본값 파일명

            ExcelOpenFileDialog.Multiselect = true;                      //여러파일선택
            ExcelOpenFileDialog.ReadOnlyChecked = true;                  //읽기전용으로 열것인지체크
            ExcelOpenFileDialog.ShowReadOnly = true;                     //읽기전용파일 보이기 

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