using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic; // ����List����������ռ�

public class ExportCollider : MonoBehaviour
{
    // ��Ҫ���������
    public GameObject[] objects;

    // excel�ļ�·��
    public string filePath = "C:\\Users\\BUG\\Desktop\\ColliderRecorder.xlsx";

    // ��ʱ��
    private float timer = 0;

    // �����ԭʼcollider��С
    private Vector3[] originalSizes;

    // �û������Ԫ������
    public int[] elementSizes;

    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ�������б�
        List<GameObject> objectList = new List<GameObject>(); // ����һ���յ�List

        // ����Ԫ���������飬�ҵ����ڵ����岢��ӵ��б���
        for (int i = 0; i < elementSizes.Length; i++) // ���ѭ��������Ԫ����������
        {
            for (int j = 0; j < elementSizes[i]; j++) // �ڲ�ѭ��������ÿ��Ԫ�صĴ�С
            {
                // �������������ҵ���Ӧ������
                string objectName = (i + 1) + "." + (j + 1); // ��������ƣ���ʽΪa.b
                GameObject obj = GameObject.Find(objectName); // ͨ�����Ʋ�������
                // ����ҵ������壬����ӵ��б���
                if (obj != null)
                {
                    objectList.Add(obj); // ��ӵ��б���
                }
                // ���û���ҵ����壬����������
                else
                {
                    Debug.Log("������Ϊ" + objectName + "������");
                }
            }
        }

        // ���б�ת������������
        objects = objectList.ToArray(); // ����List��ToArray����

        // ��ʼ��ԭʼcollider��С����
        originalSizes = new Vector3[objects.Length];

        // �����������飬��ȡÿ�������ԭʼcollider��С
        for (int i = 0; i < objects.Length; i++)
        {
            // ���������collider������ͻ�ȡ���Ĵ�С
            if (objects[i].GetComponent<Collider>() != null)
            {
                originalSizes[i] = objects[i].GetComponent<Collider>().bounds.size;
            }
            // ���򣬾�����Ϊ������
            else
            {
                originalSizes[i] = Vector3.zero;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ÿ��1�����һ��excel�ļ�
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            ExportXlsxFile();
        }
    }

    // ���excel�ļ��ķ���
    private void ExportXlsxFile()
    {
        // ���ñ���·��
        FileInfo newFile = new FileInfo(filePath);
        if (newFile.Exists)
        {
            // �Ѵ��� �͸���
            newFile.Delete();
            newFile = new FileInfo(filePath);
        }
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            // ���sheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
            // ���õ�Ԫ���ʽΪ������
            worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            // ���õ�Ԫ����
            for (int i = 1; i <= 3; i++)
            {
                worksheet.Column(i).Width = 20;
            };

            // ���õ�Ԫ�������
            worksheet.Cells[1, 1].Value = "��������"; // ����
            worksheet.Cells[1, 2].Value = "ԭʼ��С"; // ����
            worksheet.Cells[1, 3].Value = "���մ�С"; // ����
            for (int i = 0; i < objects.Length; i++)
            {
                worksheet.Cells[i + 2, 1].Value = objects[i].name; // ��������
                worksheet.Cells[i + 2, 2].Value = originalSizes[i].ToString(); // ����ԭʼcollider��С
                // ���������collider������ͻ�ȡ�������մ�С
                if (objects[i].GetComponent<Collider>() != null)
                {
                    worksheet.Cells[i + 2, 3].Value = objects[i].GetComponent<Collider>().bounds.size.ToString(); // ��������collider��С
                }
                // ���򣬾�����Ϊ������
                else
                {
                    worksheet.Cells[i + 2, 3].Value = Vector3.zero.ToString(); // ��������collider��С
                }
            }

            // ����
            package.Save();
        }
    }
}
