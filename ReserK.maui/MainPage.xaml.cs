using CommunityToolkit.Maui.Storage;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace ReserK.maui
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TableRow> DischargesData { get; set; } = new();

        List<string> linesTitles1 = new List<string> { "Уровень в УР, Z", "Давление в деривации, Hд" };
        List<string> linesTitles2 = new List<string> { "Расход турбинных водоводов, Qт", "Расход деривации, Qд"  };

        List<string> AxisTitles1 = new List<string> { "Время, с", "м" };
        List<string> AxisTitles2 = new List<string> { "Время, с", "м³/с" };

        // Страница с проектом и помощью
        private Uri uri = new Uri("https://github.com/electronik779/ReserK");

        int dischargeLowCount = 3;
        int step = 0;
        double[,] resultData = new double[0, 0];

        double diversionLenght = 0,
                diversionArea = 0,
                diversionRoughnessFactor = 0,
                surgeTankArea = 0,
                surgeTankAadditionalResistance = 0,
            surgeTankSpillwayElewation = 0,
            surgeTankSpillwayLenght = 0,
            surgeTankSpillwayDischargeCoefficient = 0,
            surgeTankLowerChamberTopElevation = 0,
            surgeTankLowerChamberBottomElevation = 0,
            surgeTankLowerChamberArea = 0,
                timeStep = 0;

        public MainPage()
        {
            InitializeComponent();
            saveResults_button.IsEnabled = false;
            InitializeDischargeTable();
            InitializeGraphs();
        }

        private void InitializeDischargeTable()
        {
            DischargesData.Clear();

            var inputTime = new TableRow();
            inputTime.Index = 0;
            inputTime.IsEditable = true;
            inputTime.RowLabel = "Время, с";
            inputTime.InitializeCells(dischargeLowCount, "0");
            DischargesData.Add(inputTime);

            var inputDischarge = new TableRow();
            inputDischarge.Index = 1;
            inputDischarge.IsEditable = true;
            inputDischarge.RowLabel = "Расход, м³/с";
            inputDischarge.InitializeCells(dischargeLowCount, "0");
            DischargesData.Add(inputDischarge);

            dischargeLow.ItemsSource = null;
            dischargeLow.ItemsSource = DischargesData;
        }

        private void InitializeGraphs()
        {
            double[,] zeroData = new double[,]
            {
                { 0, 1 },
                { 0, 0 },
                { 0, 0 }
            };

            CreateGraph(HeadSeries, zeroData, linesTitles1, AxisTitles1);
            CreateGraph(DischSeries, zeroData, linesTitles2, AxisTitles2);
        }

        private async void Open_Click(object sender, EventArgs e)
        {
            var culture = CultureInfo.InvariantCulture;
            try
            {
                // Выбор файла
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите CSV файл",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.WinUI, new[] { ".csv" } },
                        { DevicePlatform.MacCatalyst, new[] { "csv" } }
                    })
                });

                if (result == null) return;

                var blocks = new List<List<string>>();
                using (var reader = new StreamReader(await result.OpenReadAsync()))
                {
                    string? line;
                    // Читаем, пока ReadLineAsync не вернет null
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            blocks.Add(line.Split(';').Select(s => s.Trim()).ToList());
                        }
                    }
                }
                if (blocks.Count < 6)
                {
                    await DisplayAlertAsync("Ошибка!", "Файл поврежден.", "ОК");
                    return;
                }

                // Блок 1: Одиночные поля
                var b1 = blocks[0];
                ld.Text = b1.ElementAtOrDefault(0) ?? "0";
                fd.Text = b1.ElementAtOrDefault(1) ?? "0";
                nd.Text = b1.ElementAtOrDefault(2) ?? "0";

                var b2 = blocks[1];
                fr.Text = b2.ElementAtOrDefault(0) ?? "0";
                dz.Text = b2.ElementAtOrDefault(1) ?? "0";

                var b3 = blocks[2];
                zvod.Text = b3.ElementAtOrDefault(0) ?? "0";
                bvod.Text = b3.ElementAtOrDefault(1) ?? "0";
                mvod.Text = b3.ElementAtOrDefault(2) ?? "0";

                var b4 = blocks[3];
                zvnk.Text = b4.ElementAtOrDefault(0) ?? "0";
                znnk.Text = b4.ElementAtOrDefault(1) ?? "0";
                fnk.Text = b4.ElementAtOrDefault(2) ?? "0";

                var b5 = blocks[4];
                var timeLow = b5.Take(dischargeLowCount).ToList();
                var dischargeLow = b5.Skip(dischargeLowCount).Take(dischargeLowCount).ToList();
                for (int i = 0; i < dischargeLowCount; i++)
                {
                    // Строка 0
                    string rawValue1 = timeLow[i];
                    string processedValue1 = "0";
                    if (!string.IsNullOrWhiteSpace(rawValue1))
                    {
                        rawValue1 = rawValue1.Replace(',', '.');
                        if (double.TryParse(rawValue1, NumberStyles.Any, culture, out double value1))
                        {
                            processedValue1 = value1.ToString(culture);
                        }
                    }
                    DischargesData[0].SetCell(i, processedValue1);

                    // Строка 1
                    string? rawValue2 = i < dischargeLowCount ? dischargeLow[i] : null;
                    string processedValue2 = "0";
                    if (!string.IsNullOrWhiteSpace(rawValue2))
                    {
                        rawValue2 = rawValue2.Replace(',', '.');
                        if (double.TryParse(rawValue2, NumberStyles.Any, culture, out double value2))
                        {
                            processedValue2 = value2.ToString(culture);
                        }
                    }
                    DischargesData[1].SetCell(i, processedValue2);
                }

                var b6 = blocks[5];
                dt.Text = b6.ElementAtOrDefault(0) ?? "0";
                tr.Text = b6.ElementAtOrDefault(1) ?? "0";
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Ошибка чтения файла!", ex.Message, "OK");
            }
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            try
            {
                var culture = CultureInfo.InvariantCulture; // Точка всегда будет разделителем

                string b1 = string.Join(";", new[] {
                    ld.Text.ToString(culture),
                    fd.Text.ToString(culture),
                    nd.Text.ToString(culture)
                });

                string b2 = string.Join(";", new[] {
                    fr.Text.ToString(culture),
                    dz.Text.ToString(culture)
                });

                string b3 = string.Join(";", new[] {
                    zvod.Text.ToString(culture),
                    bvod.Text.ToString(culture),
                    mvod.Text.ToString(culture)
                });

                string b4 = string.Join(";", new[] {
                    zvnk.Text.ToString(culture),
                    znnk.Text.ToString(culture),
                    fnk.Text.ToString(culture)
                });

                string b5 = GetRowData(DischargesData, 0, dischargeLowCount) + ";" +
                            GetRowData(DischargesData, 1, dischargeLowCount);

                string b6 = string.Join(";", new[] {
                    dt.Text.ToString(culture),
                    tr.Text.ToString(culture)
                });

                // Сборка и сохранение
                string content = string.Join(Environment.NewLine, new[] { b1, b2, b3, b4, b5, b6 });

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                var result = await FileSaver.Default.SaveAsync("Initial_data.csv", stream, CancellationToken.None);

                if (result.IsSuccessful)
                    await DisplayAlertAsync(
                        "Успех!",
                        $"Файл сохранен: {result.FilePath}\n(разделитель - точка с запятой).",
                        "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(
                    "Ошибка!",
                    "Ошибка записи: " + ex.Message,
                    "OK");
            }
        }

        private string GetRowData(ObservableCollection<TableRow> table, int rowIndex, int count)
        {
            var culture = CultureInfo.InvariantCulture;

            // Проверяем, существует ли вообще строка с таким индексом в таблице
            if (rowIndex >= table.Count)
            {
                // Возвращаем строку из нулей, если строки нет
                return string.Join(";", Enumerable.Repeat("0", count));
            }

            var row = table[rowIndex];

            return string.Join(";", Enumerable.Range(0, count).Select(cellIndex =>
            {
                // Проверяем, что индекс ячейки не выходит за границы списка Cells
                if (cellIndex < row.Cells.Count)
                {
                    string rawValue = row.Cells[cellIndex];

                    if (!string.IsNullOrWhiteSpace(rawValue))
                    {
                        // Принудительно меняем запятую на точку для InvariantCulture
                        rawValue = rawValue.Replace(',', '.');

                        if (double.TryParse(rawValue, NumberStyles.Any, culture, out double val))
                        {
                            return val.ToString(culture);
                        }
                    }
                }
                return "0"; // Если ячейки нет или там не число
            }));
        }


        private async void Execute_Click(object sender, EventArgs e)
        {
            double
                diversionDiameter = 0,
                diversionLossFactor = 0,
                diversionDischarge = 0,
                diversionHeadLoss = 0,
                diversionPressure = 0,
                surgeTankLossFactor = 0,
                surgeTankDischarge = 0,
                surgeTankHeadLoss = 0,
                surgeTankElevation = 0,
                surgeTankUppersChamberVolume = 0,
                surgeTankVolume = 0,
                hydraulicRadius = 0,
                coefficientSchezi = 0,
                penstockDischarge = 0,
                timeCount;
            double[,] dischargeLow = new double[2, 6];

            try
            {
                var culture = CultureInfo.InvariantCulture;

                if (!double.TryParse(ld.Text?.Replace(',', '.'), NumberStyles.Any, culture, out diversionLenght)) return;
                if (!double.TryParse(fd.Text?.Replace(',', '.'), NumberStyles.Any, culture, out diversionArea)) return;
                if (!double.TryParse(nd.Text?.Replace(',', '.'), NumberStyles.Any, culture, out diversionRoughnessFactor)) return;
                if (!double.TryParse(fr.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankArea)) return;
                if (!double.TryParse(dz.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankAadditionalResistance)) return;
                if (!double.TryParse(zvod.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankSpillwayElewation)) return;
                if (!double.TryParse(bvod.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankSpillwayLenght)) return;
                if (!double.TryParse(mvod.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankSpillwayDischargeCoefficient)) return;
                if (!double.TryParse(zvnk.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankLowerChamberTopElevation)) return;
                if (!double.TryParse(znnk.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankLowerChamberBottomElevation)) return;
                if (!double.TryParse(fnk.Text?.Replace(',', '.'), NumberStyles.Any, culture, out surgeTankLowerChamberArea)) return;
                if (!double.TryParse(dt.Text?.Replace(',', '.'), NumberStyles.Any, culture, out timeStep)) return;
                if (!double.TryParse(tr.Text?.Replace(',', '.'), NumberStyles.Any, culture, out timeCount)) return;
            }
            catch
            {
                await DisplayAlertAsync("Ошибка!",
                    "Проверьте введенные данные",
                    "OK");
                return;
            }

            try
            {
                var culture = CultureInfo.InvariantCulture;

                for (int r = 0; r < 2; r++)
                {
                    var row = DischargesData[r];

                    if (row.Cells == null || row.Cells.Count == 0)
                    {
                        continue;
                    }

                    for (int c = 0; c < dischargeLowCount; c++)
                    {
                        if (c >= row.Cells.Count)
                        {
                            break;
                        }

                        string rawValue = row.Cells[c]?.Replace(',', '.') ?? "0";

                        if (double.TryParse(rawValue, NumberStyles.Any, culture, out double parsedValue))
                        {
                            dischargeLow[r, c] = parsedValue;
                        }
                        else
                        {
                            dischargeLow[r, c] = 0.0; // Значение по умолчанию при ошибке ввода
                        }
                    }
                }
            }
            catch
            {
                await DisplayAlertAsync("Ошибка!",
                    "Проверьте введенные данные по изменению расхода.",
                    "OK");
                return;
            }

            double[,] resultGraphData = new double[0, 0];

            try
            {
                double time = 0.0;
                double diversionPressureMaximum = 0;
                double diversionPressureMinimum = 0;
                double surgeTankElevationMaximum = 0;
                double surgeTankElevationMinimum = 0;
                double surgeTankVolumeMaximum = 0;

                int stepsCount = Convert.ToInt32(timeCount / timeStep) + 2;
                step = 0;

                resultData = new double[stepsCount, 9];

                diversionDiameter = Math.Pow(4 * diversionArea / 3.1415, 0.5);
                hydraulicRadius = diversionDiameter / 4;

                if (diversionRoughnessFactor > 0)
                {
                    coefficientSchezi = 1 / diversionRoughnessFactor * Math.Pow(hydraulicRadius, 1 / 6);
                    diversionLossFactor = diversionLenght /
                        (Math.Pow(coefficientSchezi, 2) * hydraulicRadius * Math.Pow(diversionArea, 2));
                }
                else { diversionLossFactor = 0; }

                surgeTankLossFactor = surgeTankAadditionalResistance / (19.62 * Math.Pow(diversionArea, 2));

                penstockDischarge = Int11(time, dischargeLowCount, dischargeLow);
                diversionDischarge = penstockDischarge;
                surgeTankDischarge = 0;

                diversionHeadLoss = diversionLossFactor * diversionDischarge * Math.Abs(diversionDischarge) +
                        Math.Pow(diversionDischarge, 2) / (19.62 * Math.Pow(diversionArea, 2));
                surgeTankHeadLoss = 0;

                surgeTankElevation = -diversionHeadLoss;
                diversionPressure = surgeTankElevation;

                double[,] surgeTankVolumeLow = new double[,]
                {
                    { 0, surgeTankArea * (surgeTankElevation - surgeTankLowerChamberTopElevation), surgeTankArea * (surgeTankElevation - surgeTankLowerChamberTopElevation) + surgeTankLowerChamberArea * (surgeTankLowerChamberTopElevation - surgeTankLowerChamberBottomElevation), surgeTankArea * (surgeTankElevation - surgeTankLowerChamberTopElevation) + surgeTankLowerChamberArea * (surgeTankLowerChamberTopElevation - surgeTankLowerChamberBottomElevation) + surgeTankArea * (surgeTankLowerChamberBottomElevation + 1000) },
                    { surgeTankElevation, surgeTankLowerChamberTopElevation, surgeTankLowerChamberBottomElevation, -1000 }
                };

                //Debug.WriteLine($"-====== STEP 0 ======-");
                //Debug.WriteLine($"Penstock discharge = {penstockDischarge}");
                //Debug.WriteLine($"Diversion discharge = {diversionDischarge}, Diversion head loss = {diversionHeadLoss}");
                //Debug.WriteLine($"Surge tank discharge = {surgeTankDischarge}, Surge tank head loss = {surgeTankHeadLoss}");
                //Debug.WriteLine($"Surge tank volume = {surgeTankVolume}, Surge tank elevation = {surgeTankElevation}");
                //Debug.WriteLine("");

                resultData[0, 0] = time;
                resultData[0, 1] = penstockDischarge;
                resultData[0, 2] = diversionDischarge;
                resultData[0, 3] = surgeTankDischarge;
                resultData[0, 4] = diversionHeadLoss;
                resultData[0, 5] = surgeTankHeadLoss;
                resultData[0, 6] = surgeTankElevation;
                resultData[0, 7] = diversionPressure;
                resultData[0, 8] = surgeTankUppersChamberVolume;

                while (time <= timeCount)
                {
                    time += timeStep;
                    step++;

                    penstockDischarge = Int11(time, dischargeLowCount, dischargeLow);
                    surgeTankDischarge = diversionDischarge - penstockDischarge;

                    diversionHeadLoss = diversionLossFactor * diversionDischarge * Math.Abs(diversionDischarge) + 
                        Math.Pow(diversionDischarge, 2) / (19.62 * Math.Pow(diversionArea, 2));
                    surgeTankHeadLoss = surgeTankLossFactor * surgeTankDischarge * Math.Abs(surgeTankDischarge);

                    //Debug.WriteLine($"-====== STEP {step} ======-");
                    //Debug.WriteLine($"Penstock discharge = {penstockDischarge}");
                    //Debug.WriteLine($"Diversion discharge = {diversionDischarge}, Diversion head loss = {diversionHeadLoss}");
                    //Debug.WriteLine($"Surge tank discharge = {surgeTankDischarge}, Surge tank head loss = {surgeTankHeadLoss}");

                    if (surgeTankDischarge <= 0)
                    {
                        surgeTankVolume += Math.Abs(surgeTankDischarge) * timeStep;
                        surgeTankElevation = Int11(surgeTankVolume, 4, surgeTankVolumeLow);
                        diversionDischarge += (-(surgeTankElevation + diversionHeadLoss + surgeTankHeadLoss) * 
                            9.81 * diversionArea / diversionLenght) * timeStep;
                        surgeTankDischarge = diversionDischarge - penstockDischarge;
                        diversionPressure = surgeTankElevation + surgeTankHeadLoss;

                        //Debug.WriteLine($"-= Qr<=0 =-");
                        //Debug.WriteLine($"Diversion discharge = {diversionDischarge}, Diversion head loss = {diversionHeadLoss}, Diversion pressure {diversionPressure}");
                        //Debug.WriteLine($"Surge tank discharge = {surgeTankDischarge}, Surge tank head loss = {surgeTankHeadLoss}");
                        //Debug.WriteLine($"Surge tank volume = {surgeTankVolume}, Surge tank elevation = {surgeTankElevation}");

                        if (surgeTankDischarge >= 0) { break; }
                    }

                    if (surgeTankElevation <= surgeTankSpillwayElewation && surgeTankDischarge > 0)
                    {
                        surgeTankElevation += surgeTankDischarge / surgeTankArea * timeStep;
                        diversionDischarge += (-(surgeTankElevation + diversionHeadLoss + surgeTankHeadLoss) *
                            9.81 * diversionArea / diversionLenght) * timeStep;
                        surgeTankDischarge = diversionDischarge - penstockDischarge;
                        diversionPressure = surgeTankElevation + surgeTankHeadLoss;

                        //Debug.WriteLine($"-= Zr <= Zvod && Qr>0 =-");
                        //Debug.WriteLine($"Diversion discharge = {diversionDischarge}, Diversion head loss = {diversionHeadLoss}, Diversion pressure {diversionPressure}");
                        //Debug.WriteLine($"Surge tank discharge = {surgeTankDischarge}, Surge tank head loss = {surgeTankHeadLoss}");
                        //Debug.WriteLine($"Surge tank volume = {surgeTankVolume}, Surge tank elevation = {surgeTankElevation}");

                        if (surgeTankDischarge <= 0) { break; }
                    }
                    else if (surgeTankElevation > surgeTankSpillwayElewation && surgeTankDischarge > 0)
                    {
                        surgeTankElevation = surgeTankSpillwayElewation + 
                            Math.Pow((surgeTankDischarge / (surgeTankSpillwayDischargeCoefficient * 
                            surgeTankSpillwayLenght * 4.43)), 2 / 3);
                        diversionDischarge += (-(surgeTankElevation + diversionHeadLoss + surgeTankHeadLoss) *
                            9.81 * diversionArea / diversionLenght) * timeStep;
                        surgeTankDischarge = diversionDischarge - penstockDischarge;
                        diversionPressure = surgeTankElevation + surgeTankHeadLoss;
                        surgeTankUppersChamberVolume += surgeTankDischarge * timeStep;

                        //Debug.WriteLine($"-= Zr > Zvod && Qr>0 =-");
                        //Debug.WriteLine($"Diversion discharge = {diversionDischarge}, Diversion head loss = {diversionHeadLoss}, Diversion pressure {diversionPressure}");
                        //Debug.WriteLine($"Surge tank discharge = {surgeTankDischarge}, Surge tank head loss = {surgeTankHeadLoss}");
                        //Debug.WriteLine($"Surge tank volume = {surgeTankVolume}, Surge tank elevation = {surgeTankElevation}");

                        if (surgeTankDischarge <= 0) { break; }
                    }

                    resultData[step, 0] = time;
                    resultData[step, 1] = penstockDischarge;
                    resultData[step, 2] = diversionDischarge;
                    resultData[step, 3] = surgeTankDischarge;
                    resultData[step, 4] = diversionHeadLoss;
                    resultData[step, 5] = surgeTankHeadLoss;
                    resultData[step, 6] = surgeTankElevation;
                    resultData[step, 7] = diversionPressure;
                    resultData[step, 8] = surgeTankUppersChamberVolume;

                    //Debug.WriteLine("");
                }

                surgeTankElevationMaximum = resultData[0, 6];
                surgeTankElevationMinimum = resultData[0, 6];
                surgeTankVolumeMaximum = resultData[0, 8];
                diversionPressureMaximum = resultData[0, 7];
                diversionPressureMinimum = resultData[0, 7];

                // Определяем минимальный и максимальный уровень
                for (int i = 0; i < stepsCount; i++)
                {
                    if (resultData[i, 6] < surgeTankElevationMinimum)
                        surgeTankElevationMinimum = resultData[i, 6];
                    if (resultData[i, 6] > surgeTankElevationMaximum)
                        surgeTankElevationMaximum = resultData[i, 6];

                    if (resultData[i, 8] > surgeTankVolumeMaximum)
                        surgeTankVolumeMaximum = resultData[i, 8];
                }

                if (dischargeLow[1,0] > dischargeLow[1,1])
                {
                    Z1H.Text = "Zмакс, м:";
                    Z1.Text = Math.Round(surgeTankElevationMaximum, 2).ToString("F2");
                    ucv1.Text = Math.Round(surgeTankVolumeMaximum, 0).ToString("N0");
                }
                else
                {
                    Z1H.Text = "Zмин, м:";
                    Z1.Text = Math.Round(surgeTankElevationMinimum, 2).ToString("F2");
                    ucv1.Text = "-";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Ошибка расчета!",
                    $"Проверьте введенные данные {ex}",
                    "OK");
                return;
            }

            resultGraphData = new double[3, step];

            for (int i = 0; i < step; i++)
            {
                resultGraphData[0, i] = resultData[i, 0];
                resultGraphData[1, i] = resultData[i, 6];
                resultGraphData[2, i] = resultData[i, 7];
            }
            CreateGraph(HeadSeries, resultGraphData, linesTitles1, AxisTitles1);

            for (int i = 0; i < step; i++)
            {
                resultGraphData[0, i] = resultData[i, 0];
                resultGraphData[1, i] = resultData[i, 1];
                resultGraphData[2, i] = resultData[i, 2];
            }
            CreateGraph(DischSeries, resultGraphData, linesTitles2, AxisTitles2);

            saveResults_button.IsEnabled = true;
        }

        private async void Save_Results_Click(object sender, EventArgs e)
        {
            try
            {
                var csvContent = new StringBuilder();

                string[] headers = new string[]
                {
                        "Время, с", "Расход турбинных водоводов, м3/с", "Расход деривации, м3/с",
                        "Расход резервуара, м3/с", "Потери в деривации, м",
                        "Потери в резервуаре, м", "Уровень в резервуаре, м",
                        "Давление в деривации, м", "Объем в верхней камере, м3"
                };

                string headerLine = string.Join(";", headers.Select(EscapeCsvField));
                csvContent.AppendLine(headerLine);

                for (int r = 0; r < step; r++)
                {
                    string processedCells = "";
                    for (int c = 0; c < 8; c++)
                    {
                        processedCells += resultData[r, c].ToString("F2") + ";";
                    }
                    processedCells += resultData[r, 8].ToString("F2");
                    csvContent.AppendLine(processedCells);
                }

                // принудительно создаем кодировку UTF-8 с меткой BOM (true)
                var encodingWithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                byte[] fileBytes = encodingWithBom.GetBytes(csvContent.ToString());
                using var stream = new MemoryStream(fileBytes);

                // Вызываем системный диалог сохранения файла
                var fileSaverResult = await FileSaver.Default.SaveAsync("Result_data.csv", stream, CancellationToken.None);

                if (fileSaverResult.IsSuccessful)
                {
                    await DisplayAlertAsync(
                        "Успех!",
                        $"Файл успешно сохранен: {fileSaverResult.FilePath}\n(разделитель - точка с запятой)",
                        "OK"
                    );
                }
                else if (fileSaverResult.Exception != null)
                {
                    throw fileSaverResult.Exception;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(
                    "Ошибка!",
                    $"Не удалось сохранить файл: {ex.Message}",
                    "OK"
                );
            }
        }

        private async void Help_Click(object sender, EventArgs e)
        {
            try
            {
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch
            {
                // Обработка ошибок (например, если браузер не установлен на устройстве)
                await DisplayAlertAsync("Ошибка!",
                    "Не удалось открыть ссылку.\nОткройте ссылку в браузере https://github.com/electronik779/ReserK",
                    "OK");
            }

        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return string.Empty;
            }

            if (field.Contains(";") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }

        // D - текущее вход
        // N - количество точек массива
        // data - массив [2,N], 0,N - вход, 1,N - выход
        private double Int11(double D, int N, double[,] data)
        {
            double V = -1;
            int i;
            for (i = 1; i < N; i++)
            {
                if (D - data[0, i] <= 0)
                {
                    int i1 = i - 1;
                    V = (data[1, i] * (D - data[0, i1]) - data[1, i1] * (D - data[0, i])) /
                        (data[0, i] - data[0, i1]);
                    break;
                }
            }
            if (V == -1)
            {
                V = (data[1, 2] * (D - data[0, 1]) - data[1, 1] * (D - data[0, 2])) /
                    (data[0, 2] - data[0, 1]);
            }
            return V;
        }

        private void CreateGraph(CartesianChart _chartName, double[,] _data,
            List<string> _names, List<string> _axisNames)
        {
            int pointsCount = _data.GetLength(1); // Количество точек (длина по второму измерению)

            var line1Points = new List<ObservablePoint>();
            var line2Points = new List<ObservablePoint>();

            // Собираем координаты X и Y для обеих линий
            for (int i = 0; i < pointsCount; i++)
            {
                double x = _data[0, i]; // Значение по оси X общее для обеих линий
                double y1 = _data[1, i]; // Значение Y для 1-й линии
                double y2 = _data[2, i]; // Значение Y для 2-й линии

                line1Points.Add(new ObservablePoint(x, y1));
                line2Points.Add(new ObservablePoint(x, y2));
            }

            // Создаем серии данных
            var seriesCollection = new ISeries[]
            {
                new LineSeries<ObservablePoint>
                {
                    Name = _names[0],
                    Values = line1Points,
                    GeometrySize = 0,
                    LineSmoothness = 0.5,
                    Fill = null
                },
                new LineSeries<ObservablePoint>
                {
                    Name = _names[1],
                    Values = line2Points,
                    GeometrySize = 0,
                    LineSmoothness = 0.5,
                    Fill = null
                }
            };

            _chartName.XAxes = new Axis[]
            {
                new Axis
                {
                    Name = _axisNames[0],
                    NameTextSize = 12,
                    TextSize = 12,

                    SeparatorsPaint = new SolidColorPaint
                    {
                        // Для Светлой темы (Light): черный цвет с прозрачностью ~9% (22 из 255)
                        Color = new SKColor(204,204,204),
                        StrokeThickness = 1f // Ровно 1 пиксель
                    }
                }
            };

            // 3. Настраиваем ось Y
            _chartName.YAxes = new Axis[]
            {
                new Axis
                {
                    Name = _axisNames[1],
                    NameTextSize = 12,
                    TextSize = 12
                }
            };

            // Привязываем созданные серии к графику на форме
            _chartName.Series = seriesCollection;
        }

        private void OnEntryFocused(object? sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                // Dispatcher дает платформе завершить внутренние процессы фокусировки,
                // после чего мы безопасно выделяем весь текст
                Dispatcher.Dispatch(() =>
                {
                    if (entry.IsFocused && !string.IsNullOrEmpty(entry.Text))
                    {
                        entry.CursorPosition = 0;
                        entry.SelectionLength = entry.Text.Length;
                    }
                });
            }
        }

        private void OnDoubleEntryTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                // 1. Ищем строку данных (TableRow) и контейнер ячеек, поднимаясь вверх по визуальному дереву
                TableRow? row = null;
                Element? current = entry.Parent;
                Layout? cellsLayout = null;

                while (current != null)
                {
                    // Первый Layout на пути вверх, в котором лежат наши Entry — это контейнер ячеек
                    if (cellsLayout == null && current is Layout layout)
                    {
                        cellsLayout = layout;
                    }

                    // Как только нашли элемент, привязанный к TableRow — мы нашли нашу строку
                    if (current.BindingContext is TableRow matchedRow)
                    {
                        row = matchedRow;
                        break;
                    }
                    current = current.Parent;
                }

                // 2. Если строка и контейнер ячеек успешно найдены
                if (row != null && cellsLayout != null)
                {
                    // Находим индекс текущего поля ввода внутри его непосредственного контейнера-родителя
                    // Для CollectionView непосредственным родителем entry может быть внутренний контейнер,
                    // поэтому надежнее искать entry или его предка внутри cellsLayout.
                    int cellIndex = -1;

                    if (cellsLayout.Children.Contains(entry))
                    {
                        cellIndex = cellsLayout.Children.IndexOf(entry);
                    }
                    else
                    {
                        // Если MAUI обернул Entry во внутренний контейнер, ищем этот контейнер
                        var visualChild = cellsLayout.Children.FirstOrDefault(c =>
                            c == entry || (c is Element el && IsAncestorOf(el, entry)));
                        if (visualChild != null)
                        {
                            cellIndex = cellsLayout.Children.IndexOf(visualChild);
                        }
                    }

                    // Сохраняем введенный текст напрямую в коллекцию Cells
                    if (cellIndex >= 0 && cellIndex < row.Cells.Count)
                    {
                        row.Cells[cellIndex] = e.NewTextValue ?? "";
                    }
                }

                // 3. Логика валидации (остается без изменений)
                string text = e.NewTextValue?.Replace(',', '.') ?? "";
                bool isValid = !string.IsNullOrWhiteSpace(text) &&
                              double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _);

                if (isValid)
                {
                    VisualStateManager.GoToState(entry, "Normal");
                }
                else
                {
                    VisualStateManager.GoToState(entry, "Invalid");
                }
            }
        }

        // Вспомогательный метод для проверки вложенности элементов (нужен для CollectionView)
        private bool IsAncestorOf(Element ancestor, Element descendent)
        {
            Element? current = descendent.Parent;
            while (current != null)
            {
                if (current == ancestor) return true;
                current = current.Parent;
            }
            return false;
        }
    }
}
