using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    public static string[] upgradeName = { "���ݷ� ��", "���ݼӵ� ��", "���ݷ� ����", "ũ��Ƽ�� ��", "���� ����" };
    public static string[] upgradeInfo = { "���ݷ��� 1 �ø��ϴ�.", "���ݼӵ��� 10% ���Դϴ�.", "�Ѿ��� 1�� �� �߻��մϴ�.", "ũ��Ƽ�� Ȯ���� 10% �ø��ϴ�.", "���� Ȯ���� 2���� ������ ���մϴ�" };
    public static Sprite[] upgradeImage = Resources.LoadAll<Sprite>("Images");
}
