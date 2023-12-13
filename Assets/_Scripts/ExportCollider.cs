using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic; // 引入List所需的命名空间

public class ExportCollider : MonoBehaviour
{
    // 需要输出的物体
    public GameObject[] objects;

    // excel文件路径
    public string filePath = "C:\\Users\\BUG\\Desktop\\ColliderRecorder.xlsx";

    // 计时器
    private float timer = 0;

    // 物体的原始collider大小
    private Vector3[] originalSizes;

    // 用户输入的元素数量
    public int[] elementSizes;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化物体列表
        List<GameObject> objectList = new List<GameObject>(); // 创建一个空的List

        // 遍历元素数量数组，找到存在的物体并添加到列表中
        for (int i = 0; i < elementSizes.Length; i++) // 外层循环，遍历元素数量数组
        {
            for (int j = 0; j < elementSizes[i]; j++) // 内层循环，遍历每个元素的大小
            {
                // 根据命名规则，找到对应的物体
                string objectName = (i + 1) + "." + (j + 1); // 物体的名称，形式为a.b
                GameObject obj = GameObject.Find(objectName); // 通过名称查找物体
                // 如果找到了物体，就添加到列表中
                if (obj != null)
                {
                    objectList.Add(obj); // 添加到列表中
                }
                // 如果没有找到物体，就跳过创建
                else
                {
                    Debug.Log("跳过名为" + objectName + "的物体");
                }
            }
        }

        // 将列表转换成物体数组
        objects = objectList.ToArray(); // 调用List的ToArray方法

        // 初始化原始collider大小数组
        originalSizes = new Vector3[objects.Length];

        // 遍历物体数组，获取每个物体的原始collider大小
        for (int i = 0; i < objects.Length; i++)
        {
            // 如果物体有collider组件，就获取它的大小
            if (objects[i].GetComponent<Collider>() != null)
            {
                originalSizes[i] = objects[i].GetComponent<Collider>().bounds.size;
            }
            // 否则，就设置为零向量
            else
            {
                originalSizes[i] = Vector3.zero;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 每隔1秒更新一次excel文件
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            ExportXlsxFile();
        }
    }

    // 输出excel文件的方法
    private void ExportXlsxFile()
    {
        // 设置保存路径
        FileInfo newFile = new FileInfo(filePath);
        if (newFile.Exists)
        {
            // 已存在 就覆盖
            newFile.Delete();
            newFile = new FileInfo(filePath);
        }
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            // 添加sheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
            // 设置单元格格式为正中心
            worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            // 设置单元格宽度
            for (int i = 1; i <= 3; i++)
            {
                worksheet.Column(i).Width = 20;
            };

            // 设置单元格的文字
            worksheet.Cells[1, 1].Value = "物体名称"; // 标题
            worksheet.Cells[1, 2].Value = "原始大小"; // 标题
            worksheet.Cells[1, 3].Value = "最终大小"; // 标题
            for (int i = 0; i < objects.Length; i++)
            {
                worksheet.Cells[i + 2, 1].Value = objects[i].name; // 物体名称
                worksheet.Cells[i + 2, 2].Value = originalSizes[i].ToString(); // 物体原始collider大小
                // 如果物体有collider组件，就获取它的最终大小
                if (objects[i].GetComponent<Collider>() != null)
                {
                    worksheet.Cells[i + 2, 3].Value = objects[i].GetComponent<Collider>().bounds.size.ToString(); // 物体最终collider大小
                }
                // 否则，就设置为零向量
                else
                {
                    worksheet.Cells[i + 2, 3].Value = Vector3.zero.ToString(); // 物体最终collider大小
                }
            }

            // 保存
            package.Save();
        }
    }
}
