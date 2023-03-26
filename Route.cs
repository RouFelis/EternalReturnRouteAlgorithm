using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public enum StartWeapon
{
    DAGGER,
    TWOHANDEDSWORD,
    AXE,
    DUALSWORDS,
    PISTOL,
    ASSAULTRIFLE,
    SNIPERRIFLE,
    RAPIER,
    SPEAR,
    HAMMER,
    BAT,
    THROW,
    SHURIKEN,
    BOW,
    CROSSBOW,
    GLOVE,
    TONFA,
    GUITAR,
    NUNCHAKU,
    WHIP,
    CAMERA
}

public class Route : MonoBehaviour
{ 
    public static List<Item> list_item = new List<Item>(); // ������ ����Ʈ ����

    #region MAP

    public static List<int>[] list_map = new List<int>[]
    {
        new List<int>() { 368, 373, 377, 383, 395, 396, 404, 407, 408, 410, 419, 420, 422, 427, 429, 431, 433, 439 }, // ����
        new List<int>() { 371, 377, 379, 383, 384, 390, 400, 403, 405, 410, 426, 427, 429, 436, 440, 443, 444, 445 }, // �����
        new List<int>() { 369, 377, 383, 385, 388, 399, 406, 409, 411, 415, 422, 426, 427, 434, 436, 437, 442, 444 }, // ��ȭ��
        new List<int>() { 373, 374, 375, 377, 383, 395, 401, 404, 420, 425, 427, 428, 433, 438, 442, 445, 447 }, //�𷡻���
        new List<int>() { 374, 377, 379, 381, 383, 395, 399, 405, 412, 413, 423, 424, 427, 441, 443, 444, 446 }, // ����
        new List<int>() { 371, 377, 383, 385, 387, 389, 390, 391, 401, 415, 417, 418, 419, 423, 427, 440, 447 }, // ����
        new List<int>() { 370, 371, 377, 378, 383, 385, 394, 404, 407, 409, 417, 427, 428, 429, 430, 431, 434, 444, 445 }, // �ױ�
        new List<int>() { 375, 377, 380, 383, 384, 386, 389, 392, 393, 420, 426, 427, 430, 431, 434, 435, 436, 439 }, // ����
        new List<int>() { 374, 376, 377, 379, 381, 383, 384, 391, 394, 411, 412, 413, 415, 421, 422, 423, 427, 438, 441, 445 }, // ��
        new List<int>() { 368, 377, 382, 383, 387, 396, 407, 411, 412, 424, 427, 430, 432, 435, 439, 444 }, // ����
        new List<int>() { 370, 377, 382, 383, 388, 392, 393, 396, 402, 406, 420, 423, 424, 425, 427, 430, 437, 441, 447 }, // ȣ��
        new List<int>() { 373, 374, 375, 376, 377, 378, 379, 383, 400, 408, 413, 414, 417, 421, 423, 427, 428, 438, 445, 446 }, // ����
        new List<int>() { }, // ������
        new List<int>() { 368, 369, 377, 380, 383, 387, 389, 400, 401, 402, 407, 409, 414, 427, 431, 432, 435, 442, 444 }, //�б�
        new List<int>() { 370, 376, 377, 378, 379, 383, 399, 403, 405, 416, 421, 427, 433, 437, 438, 440, 443 }, // ��
        new List<int>() { 369, 377, 383, 386, 393, 402, 406, 408, 410, 413, 414, 425, 427, 432, 436, 444, 447 } // ��� ���ð�
    };

    public static List<string> list_mapName = new List<string>
    {
        "����",
        "�����",
        "��ȭ��",
        "�𷡻���",
        "����",
        "����",
        "�ױ�",
        "����",
        "��",
        "����",
        "ȣ��",
        "����",
        "������",
        "�б�",
        "��",
        "��� ���ð�"
    };

    private bool[] isSelected = new bool[16] // ���� ���ÿ���
    { 
        false, 
        false, 
        false,
        false,
        false,
        false, 
        false, 
        false, 
        false,
        false, 
        false, 
        false, 
        false, 
        false, 
        false,
        false 
    };

    public static List<int> list_mapNum = new List<int>(); // ���õ� �� ���� ����Ʈ

    #endregion

    public StartWeapon weapon;

    public int[] itemSlot = new int[6] // ��ǥ ������ ����
    { 
        0, 
        0,
        0,
        0,
        0, 
        0 
    };

    public static List<int> list_itemGuide = new List<int>(); // ���� ��ǥ ������ ����Ʈ
    public static List<int> list_inventory = new List<int>(); // �κ��丮 ����Ʈ

    public List<string> list_RouteInfo = new List<string>();

    public Text[] txtMapNum = new Text[16];
    public Text txtRouteInfo;
    public Text txtMapMaterial;

    void Awake()
    {
        // ������ ����Ʈ (��ü 614�� �� 'Ư�� ��� �����۰� ������ ���ۿ� ������� �������� ����'�� 448�� ����)
        // list_item.Add(new Item(INDEX, NAME, ITEMTYPE, QUANTITY, L_NODE, R_NODE)); ������ ������ �߰� ���
        #region LIST_ITEM

        list_item.Add(new Item(0, "�ϸ���", "���", 1, 155, 251));
        list_item.Add(new Item(1, "�Ʊ״�", "���", 1, 78, 348));
        list_item.Add(new Item(2, "�̽�ƿ����", "���", 1, 89, 377));
        list_item.Add(new Item(3, "���̴�", "���", 1, 125, 263));
        list_item.Add(new Item(4, "EOD ����", "���", 1, 328, 241));
        list_item.Add(new Item(5, "ī������", "���", 1, 139, 330));
        list_item.Add(new Item(6, "�Ļ��", "���", 1, 139, 351));
        list_item.Add(new Item(7, "������������", "���", 1, 269, 262));
        list_item.Add(new Item(8, "�ٸ���Ŀ�� ����", "���", 1, 140, 360));
        list_item.Add(new Item(9, "���϶���", "���", 1, 141, 255));
        list_item.Add(new Item(10, "�Ѿ� �쿣", "���", 1, 145, 311));
        list_item.Add(new Item(11, "�Ʒд���Ʈ", "���", 1, 143, 419));
        list_item.Add(new Item(12, "����Į����", "���", 1, 146, 418));
        list_item.Add(new Item(13, "���ȣ���ڿ�", "���", 1, 144, 353));
        list_item.Add(new Item(14, "ȣǪ���", "���", 1, 146, 332));
        list_item.Add(new Item(15, "��õ�Ϸ�", "���", 1, 148, 360));
        list_item.Add(new Item(16, "�Ƽ���", "���", 1, 148, 335));
        list_item.Add(new Item(17, "����������", "���", 1, 273, 264));
        list_item.Add(new Item(18, "���̰� ����", "���", 1, 149, 351));
        list_item.Add(new Item(19, "���ƺ�", "���", 1, 150, 356));
        list_item.Add(new Item(20, "�ٱ״��� ��ġ", "���", 1, 151, 330));
        list_item.Add(new Item(21, "�丣�� ��ġ", "���", 1, 151, 264));
        list_item.Add(new Item(22, "õ����", "���", 1, 152, 335));
        list_item.Add(new Item(23, "�ݰ���", "���", 1, 163, 263));
        list_item.Add(new Item(24, "�� ����", "���", 1, 155, 432));
        list_item.Add(new Item(25, "��Ÿ ��������", "���", 1, 154, 358));
        list_item.Add(new Item(26, "������", "���", 1, 154, 355));
        list_item.Add(new Item(27, "�Ķ�", "���", 1, 153, 335));
        list_item.Add(new Item(28, "���ų�Ʈ", "���", 1, 153, 262));
        list_item.Add(new Item(29, "�ְ�â", "���", 1, 159, 350));
        list_item.Add(new Item(30, "���Ȼ��", "���", 1, 159, 264));
        list_item.Add(new Item(31, "Ʈ�����̳�", "���", 1, 156, 157));
        list_item.Add(new Item(32, "��õȭ��", "���", 1, 158, 338));
        list_item.Add(new Item(33, "û������", "���", 1, 158, 354));
        list_item.Add(new Item(34, "������ ���Ż�", "���", 1, 162, 333));
        list_item.Add(new Item(35, "Ÿ����", "���", 1, 160, 352));
        list_item.Add(new Item(36, "�������� ���", "���", 1, 161, 351));
        list_item.Add(new Item(37, "������", "���", 1, 165, 355));
        list_item.Add(new Item(38, "�۷������ϸ�", "���", 1, 164, 360));
        list_item.Add(new Item(39, "�ö�� ��", "���", 1, 165, 432));
        list_item.Add(new Item(40, "�ܿ���õ��", "���", 1, 168, 333));
        list_item.Add(new Item(41, "����� �ǽ�Ʈ", "���", 1, 169, 419));
        list_item.Add(new Item(42, "������ ��Ŭ", "���", 1, 167, 358));
        list_item.Add(new Item(43, "��ȭ������", "���", 1, 171, 424));
        list_item.Add(new Item(44, "��������", "���", 1, 171, 335));
        list_item.Add(new Item(45, "�극���� ��Ʋ��", "���", 1, 168, 347));
        list_item.Add(new Item(46, "�Ҽ�", "���", 1, 170, 350));
        list_item.Add(new Item(47, "��Ƽ�� ����", "���", 1, 173, 353));
        list_item.Add(new Item(48, "���̽�", "���", 1, 174, 378));
        list_item.Add(new Item(49, "�ö�� ����", "���", 1, 174, 433));
        list_item.Add(new Item(50, "����ź", "���", 1, 285, 177));
        list_item.Add(new Item(51, "��Ƽ��ũ�� ����ź", "���", 1, 54, 419));
        list_item.Add(new Item(52, "�ٺ�彽��", "���", 1, 175, 330));
        list_item.Add(new Item(53, "����ź", "���", 1, 176, 345));
        list_item.Add(new Item(54, "���� ����ź", "���", 1, 284, 266));
        list_item.Add(new Item(55, "���̾� ��", "���", 1, 286, 268));
        list_item.Add(new Item(56, "�ƽ�Ʈ����", "���", 1, 178, 332));
        list_item.Add(new Item(57, "���״� ����", "���", 1, 179, 355));
        list_item.Add(new Item(58, "��ġ���̿��� ī��", "���", 1, 288, 264));
        list_item.Add(new Item(59, "������", "���", 1, 180, 355));
        list_item.Add(new Item(60, "ǳ�� ������", "���", 1, 181, 435));
        list_item.Add(new Item(61, "Ǫ���� �ܵ�", "���", 1, 183, 261));
        list_item.Add(new Item(62, "�÷���", "���", 1, 186, 350));
        list_item.Add(new Item(63, "�ǰ��", "���", 1, 185, 379));
        list_item.Add(new Item(64, "����", "���", 1, 189, 379));
        list_item.Add(new Item(65, "��緡�� ����", "���", 1, 191, 355));
        list_item.Add(new Item(66, "Ʈ������", "���", 1, 188, 187));
        list_item.Add(new Item(67, "������ Ȱ", "���", 1, 190, 247));
        list_item.Add(new Item(68, "������Ż ����", "���", 1, 192, 244));
        list_item.Add(new Item(69, "��Ȳ", "���", 1, 196, 347));
        list_item.Add(new Item(70, "�߸���Ÿ", "���", 1, 195, 376));
        list_item.Add(new Item(71, "���� ũ�ν�����", "���", 1, 194, 334));
        list_item.Add(new Item(72, "�����ݱ�ű��", "���", 1, 193, 266));
        list_item.Add(new Item(73, "��������", "���", 1, 197, 337));
        list_item.Add(new Item(74, "�Ϸ�Ʈ�� �����", "���", 1, 296, 264));
        list_item.Add(new Item(75, "�ű׳�-����", "���", 1, 199, 346));
        list_item.Add(new Item(76, "�۷� 48", "���", 1, 197, 267));
        list_item.Add(new Item(77, "�����ǵ�", "���", 1, 200, 355));
        list_item.Add(new Item(78, "��Ʋ�� ��", "���", 1, 297, 262));
        list_item.Add(new Item(79, "95�� �ڵ� ����", "���", 1, 201, 346));
        list_item.Add(new Item(80, "AK-12", "���", 1, 201, 263));
        list_item.Add(new Item(81, "XCR", "���", 1, 202, 246));
        list_item.Add(new Item(82, "Tac-50", "���", 1, 203, 353));
        list_item.Add(new Item(83, "���ͺ���", "���", 1, 203, 364));
        list_item.Add(new Item(84, "NTW-20", "���", 1, 204, 354));
        list_item.Add(new Item(85, "���󸮽�", "���", 1, 205, 350));
        list_item.Add(new Item(86, "��ҹݰ��", "���", 1, 206, 351));
        list_item.Add(new Item(87, "����������ũ", "���", 1, 207, 262));
        list_item.Add(new Item(88, "�ɸ����ν�", "���", 1, 206, 323));
        list_item.Add(new Item(89, "Ȱ���", "���", 1, 300, 234));
        list_item.Add(new Item(90, "�෣�� MK2", "���", 1, 208, 431));
        list_item.Add(new Item(91, "��ƽ����", "���", 1, 208, 352));
        list_item.Add(new Item(92, "���� �Ҽ�", "���", 1, 209, 238));
        list_item.Add(new Item(93, "����̾�", "���", 1, 210, 388));
        list_item.Add(new Item(94, "õ���� ���", "���", 1, 211, 418));
        list_item.Add(new Item(95, "���� ������", "���", 1, 212, 351));
        list_item.Add(new Item(96, "��Ƽ���Ѽ�", "���", 1, 213, 389));
        list_item.Add(new Item(97, "�� ��", "���", 1, 214, 350));
        list_item.Add(new Item(98, "ƾ ���Ǹ�", "���", 1, 215, 335));
        list_item.Add(new Item(99, "�̷�����", "���", 1, 216, 355));
        list_item.Add(new Item(100, "���Ŀ�� ����Ʈ", "���", 1, 217, 413));
        list_item.Add(new Item(101, "ī�޶� ĳ��", "���", 1, 218, 246));
        list_item.Add(new Item(102, "V.I.C.G", "���", 1, 303, 265));
        list_item.Add(new Item(103, "���� Ƽ�ƶ�", "���", 1, 221, 332));
        list_item.Add(new Item(104, "����-OPS ���", "���", 1, 219, 352));
        list_item.Add(new Item(105, "�������� ����", "���", 1, 223, 429));
        list_item.Add(new Item(106, "���� �հ�", "���", 1, 222, 358));
        list_item.Add(new Item(107, "Ȳ�� �θ����", "���", 1, 223, 355));
        list_item.Add(new Item(108, "��ȣũ ���", "���", 1, 220, 360));
        list_item.Add(new Item(109, "��������", "���", 1, 220, 361));
        list_item.Add(new Item(110, "���̾Ƶ�", "���", 1, 305, 239));
        list_item.Add(new Item(111, "���й�ä ��Ʈ", "���", 1, 316, 263));
        list_item.Add(new Item(112, "��Ŀ�� ����", "���", 1, 225, 363));
        list_item.Add(new Item(113, "������� ����", "���", 1, 229, 330));
        list_item.Add(new Item(114, "�Ƹ����׽� �Ƹ�", "���", 1, 229, 315));
        list_item.Add(new Item(115, "���� ����", "���", 1, 228, 311));
        list_item.Add(new Item(116, "���ְ��� ����", "���", 1, 226, 355));
        list_item.Add(new Item(117, "���纹", "���", 1, 227, 337));
        list_item.Add(new Item(118, "��Ʋ ��Ʈ", "���", 1, 232, 316));
        list_item.Add(new Item(119, "EOD ��Ʈ", "���", 1, 232, 313));
        list_item.Add(new Item(120, "�νõ�", "���", 1, 227, 314));
        list_item.Add(new Item(121, "�������� ����", "���", 1, 230, 260));
        list_item.Add(new Item(122, "â�Ŀ�", "���", 1, 228, 259));
        list_item.Add(new Item(123, "�ҵ� ������", "���", 1, 237, 426));
        list_item.Add(new Item(124, "�������ϸ�", "���", 1, 236, 320));
        list_item.Add(new Item(125, "����Ż ����", "���", 1, 406, 265));
        list_item.Add(new Item(126, "����� ����", "���", 1, 240, 319));
        list_item.Add(new Item(127, "�������� ����", "���", 1, 235, 358));
        list_item.Add(new Item(128, "���̱⽺", "���", 1, 238, 318));
        list_item.Add(new Item(129, "ƾ�޷ν��� ����", "���", 1, 236, 321));
        list_item.Add(new Item(130, "�����ð���", "���", 1, 319, 258));
        list_item.Add(new Item(131, "�ö�� ��ũ", "���", 1, 240, 352));
        list_item.Add(new Item(132, "����Ʈ ���", "���", 1, 239, 353));
        list_item.Add(new Item(133, "�淮ȭ ����", "���", 1, 242, 412));
        list_item.Add(new Item(134, "�Ź��� ����", "���", 1, 325, 265));
        list_item.Add(new Item(135, "ǳȭ��", "���", 1, 327, 351));
        list_item.Add(new Item(136, "�����ȷν�", "���", 1, 243, 323));
        list_item.Add(new Item(137, "Ŭ���� ����", "���", 1, 242, 426));
        list_item.Add(new Item(138, "ŸŰ�� �극�̽�", "���", 1, 322, 262));
        list_item.Add(new Item(139, "���Į", "���", 1, 269, 413));
        list_item.Add(new Item(140, "������ �ƹ� ������", "���", 1, 270, 368));
        list_item.Add(new Item(141, "ī�����̽� īŸ��", "���", 1, 271, 428));
        list_item.Add(new Item(142, "�Ϻ���", "���", 1, 371, 354));
        list_item.Add(new Item(143, "���繫��", "���", 1, 142, 436));
        list_item.Add(new Item(144, "���󸶻�", "���", 1, 142, 438));
        list_item.Add(new Item(145, "�ٽ�Ÿ�� �ҵ�", "���", 1, 371, 346));
        list_item.Add(new Item(146, "����", "���", 1, 272, 358));
        list_item.Add(new Item(147, "�ö�� �ҵ�", "���", 1, 145, 432));
        list_item.Add(new Item(148, "�Ƿ�ü�� �ְ�", "���", 1, 372, 353));
        list_item.Add(new Item(149, "�ֵ��� ��", "���", 1, 372, 235));
        list_item.Add(new Item(150, "��� ��Ÿ", "���", 1, 274, 384));
        list_item.Add(new Item(151, "�罿 ��ġ", "���", 1, 274, 427));
        list_item.Add(new Item(152, "����� ��ġ", "���", 1, 373, 331));
        list_item.Add(new Item(153, "�淮ȭ ����", "���", 1, 276, 412));
        list_item.Add(new Item(154, "����� ��", "���", 1, 275, 378));
        list_item.Add(new Item(155, "���", "���", 1, 276, 346));
        list_item.Add(new Item(156, "���̴�Ʈ", "���", 1, 277, 370));
        list_item.Add(new Item(157, "����ũ", "���", 1, 376, 346));
        list_item.Add(new Item(158, "����â", "���", 1, 375, 157));
        list_item.Add(new Item(159, "��â", "���", 1, 277, 412));
        list_item.Add(new Item(160, "������ �����", "���", 1, 278, 426));
        list_item.Add(new Item(161, "���", "���", 1, 278, 415));
        list_item.Add(new Item(162, "ȶ��", "���", 1, 378, 347));
        list_item.Add(new Item(163, "������", "���", 1, 378, 346));
        list_item.Add(new Item(164, "�ٶ� ä��", "���", 1, 279, 412));
        list_item.Add(new Item(165, "������", "���", 1, 280, 349));
        list_item.Add(new Item(166, "��Ʋ��", "���", 1, 382, 346));
        list_item.Add(new Item(167, "�� ��Ŭ", "���", 1, 282, 412));
        list_item.Add(new Item(168, "�Ͱ� �尩", "���", 1, 166, 428));
        list_item.Add(new Item(169, "���±���", "���", 1, 166, 443));
        list_item.Add(new Item(170, "���� ��Ŭ", "���", 1, 282, 332));
        list_item.Add(new Item(171, "ȸ�� �尩", "���", 1, 281, 351));
        list_item.Add(new Item(172, "������", "���", 1, 283, 433));
        list_item.Add(new Item(173, "��ť����", "���", 1, 283, 350));
        list_item.Add(new Item(174, "����", "���", 1, 286, 429));
        list_item.Add(new Item(175, "�а��� ��ź", "���", 1, 350, 285));
        list_item.Add(new Item(176, "�� ����Ʈ��", "���", 1, 384, 349));
        list_item.Add(new Item(177, "�÷���", "���", 1, 429, 343));
        list_item.Add(new Item(178, "�ʷ�", "���", 1, 376, 341));
        list_item.Add(new Item(179, "���� ���ʺ�", "���", 1, 177, 426));
        list_item.Add(new Item(180, "����", "���", 1, 288, 389));
        list_item.Add(new Item(181, "������", "���", 1, 183, 377));
        list_item.Add(new Item(182, "íũ��", "���", 1, 289, 433));
        list_item.Add(new Item(183, "��ȭ��ǥ", "���", 1, 290, 413));
        list_item.Add(new Item(184, "��ħ", "���", 1, 396, 261));
        list_item.Add(new Item(185, "����", "���", 1, 182, 416));
        list_item.Add(new Item(186, "�÷���Ÿ", "���", 1, 287, 346));
        list_item.Add(new Item(187, "�������� ����", "���", 1, 292, 426));
        list_item.Add(new Item(188, "����", "���", 1, 291, 436));
        list_item.Add(new Item(189, "����", "���", 1, 190, 433));
        list_item.Add(new Item(190, "���±�", "���", 1, 292, 443));
        list_item.Add(new Item(191, "ź��", "���", 1, 291, 356));
        list_item.Add(new Item(192, "ȭ��", "���", 1, 292, 431));
        list_item.Add(new Item(193, "��", "���", 1, 293, 429));
        list_item.Add(new Item(194, "���ݱ�", "���", 1, 294, 432));
        list_item.Add(new Item(195, "��� ũ�ν�����", "���", 1, 293, 346));
        list_item.Add(new Item(196, "ö��", "���", 1, 294, 354));
        list_item.Add(new Item(197, "FN57", "���", 1, 296, 432));
        list_item.Add(new Item(198, "���� ������ SP", "���", 1, 295, 392));
        list_item.Add(new Item(199, "�ű׳�-�Ƴ��ܴ�", "���", 1, 295, 353));
        list_item.Add(new Item(200, "������", "���", 1, 392, 362));
        list_item.Add(new Item(201, "AK-47", "���", 1, 297, 447));
        list_item.Add(new Item(202, "M16A1", "���", 1, 297, 427));
        list_item.Add(new Item(203, "��Ǭ��", "���", 1, 298, 376));
        list_item.Add(new Item(204, "�ݱ���", "���", 1, 298, 355));
        list_item.Add(new Item(205, "���ϰ�", "���", 1, 298, 352));
        list_item.Add(new Item(206, "����", "���", 1, 309, 426));
        list_item.Add(new Item(207, "����", "���", 1, 309, 387));
        list_item.Add(new Item(208, "��ȭ��", "���", 1, 300, 331));
        list_item.Add(new Item(209, "������", "���", 1, 396, 300));
        list_item.Add(new Item(210, "��� �����", "���", 1, 302, 358));
        list_item.Add(new Item(211, "���Ŀ �Ⱦ�", "���", 1, 301, 334));
        list_item.Add(new Item(212, "King-V", "���", 1, 301, 368));
        list_item.Add(new Item(213, "��ĳ����", "���", 1, 302, 353));
        list_item.Add(new Item(214, "���۽�Ʈ��", "���", 1, 302, 379));
        list_item.Add(new Item(215, "�߻���", "���", 1, 302, 436));
        list_item.Add(new Item(216, "����Ʈ ī�޶�", "���", 1, 366, 398));
        list_item.Add(new Item(217, "���������δ�", "���", 1, 334, 321));
        list_item.Add(new Item(218, "ī�޶� ������", "���", 1, 303, 393));
        list_item.Add(new Item(219, "��ź��", "���", 1, 306, 401));
        list_item.Add(new Item(220, "�ҹ� ���", "���", 1, 308, 343));
        list_item.Add(new Item(221, "Ƽ�ƶ�", "���", 1, 305, 433));
        list_item.Add(new Item(222, "�հ�", "���", 1, 306, 355));
        list_item.Add(new Item(223, "����", "���", 1, 307, 304));
        list_item.Add(new Item(224, "������� ���", "���", 1, 308, 334));
        list_item.Add(new Item(225, "���̴� ����", "���", 1, 310, 395));
        list_item.Add(new Item(226, "�罽 ����", "���", 1, 309, 395));
        list_item.Add(new Item(227, "����", "���", 1, 310, 414));
        list_item.Add(new Item(228, "ġ�Ŀ�", "���", 1, 314, 387));
        list_item.Add(new Item(229, "�Ǳ� ����", "���", 1, 405, 346));
        list_item.Add(new Item(230, "�Ѻ�", "���", 1, 313, 413));
        list_item.Add(new Item(231, "���� ������", "���", 1, 317, 347));
        list_item.Add(new Item(232, "��ź����", "���", 1, 312, 354));
        list_item.Add(new Item(233, "������ ����", "���", 1, 309, 358));
        list_item.Add(new Item(234, "�����", "���", 1, 230, 433));
        list_item.Add(new Item(235, "����", "���", 1, 427, 354));
        list_item.Add(new Item(236, "������", "���", 1, 408, 355));
        list_item.Add(new Item(237, "���ֹݵ�", "���", 1, 320, 354));
        list_item.Add(new Item(238, "��ȫ ����", "���", 1, 408, 360));
        list_item.Add(new Item(239, "�ٺ�� ��ν�", "���", 1, 413, 357));
        list_item.Add(new Item(240, "��ö ����", "���", 1, 318, 346));
        list_item.Add(new Item(241, "��ö ���� ��ȣ��", "���", 1, 322, 346));
        list_item.Add(new Item(242, "����ȭ", "���", 1, 328, 427));
        list_item.Add(new Item(243, "ų��", "���", 1, 324, 332));
        list_item.Add(new Item(244, "��켱", "���", 1, 338, 412));
        list_item.Add(new Item(245, "��ġ��", "���", 1, 333, 415));
        list_item.Add(new Item(246, "źâ", "���", 1, 417, 354));
        list_item.Add(new Item(247, "�ñ⺴�� ȭ����", "���", 1, 336, 337));
        list_item.Add(new Item(248, "���ձ�õ", "���", 1, 339, 353));
        list_item.Add(new Item(249, "������ ��ǥ", "���", 1, 340, 370));
        list_item.Add(new Item(250, "ȣũ ����", "���", 1, 341, 334));
        list_item.Add(new Item(251, "���� ���", "���", 1, 337, 340));
        list_item.Add(new Item(252, "������", "���", 1, 417, 262));
        list_item.Add(new Item(253, "�ɵ� ����", "���", 1, 246, 312));
        list_item.Add(new Item(254, "������", "���", 1, 359, 331));
        list_item.Add(new Item(255, "�����׸� ���", "���", 1, 412, 331));
        list_item.Add(new Item(256, "���ڵ����� ����", "���", 1, 417, 261));
        list_item.Add(new Item(257, "������ ���� ��", "���", 1, 335, 330));
        list_item.Add(new Item(258, "���� ����", "���", 2, 342, 385));
        list_item.Add(new Item(259, "������", "���", 5, 344, 413));
        list_item.Add(new Item(260, "��ȭ��", "���", 2, 343, 424));
        list_item.Add(new Item(261, "����", "���", 1, 351, 423));
        list_item.Add(new Item(262, "����", "���", 1, 352, 430));
        list_item.Add(new Item(263, "������", "���", 1, 332, 439));
        list_item.Add(new Item(264, "�̿� ����", "���", 1, 349, 425));
        list_item.Add(new Item(265, "�޴���", "���", 1, 353, 352));
        list_item.Add(new Item(266, "RDX", "���", 1, 362, 430));
        list_item.Add(new Item(267, "EMP ���", "���", 2, 365, 434));
        list_item.Add(new Item(268, "ȭ�� Ʈ��", "���", 1, 367, 348));
        list_item.Add(new Item(269, "���� ������", "���", 1, 370, 377));
        list_item.Add(new Item(270, "�޽�", "���", 1, 369, 387));
        list_item.Add(new Item(271, "�ڸ��ٸ�", "���", 1, 370, 381));
        list_item.Add(new Item(272, "������", "���", 1, 371, 431));
        list_item.Add(new Item(273, "������ �ְ�", "���", 1, 368, 374));
        list_item.Add(new Item(274, "���ظ�", "���", 1, 373, 378));
        list_item.Add(new Item(275, "�罽 ��", "���", 1, 374, 395));
        list_item.Add(new Item(276, "���� ����", "���", 1, 375, 379));
        list_item.Add(new Item(277, "��â", "���", 1, 376, 379));
        list_item.Add(new Item(278, "���", "���", 1, 378, 379));
        list_item.Add(new Item(279, "������", "���", 1, 380, 433));
        list_item.Add(new Item(280, "ö��", "���", 1, 380, 387));
        list_item.Add(new Item(281, "�۷���", "���", 1, 382, 427));
        list_item.Add(new Item(282, "���̾� ��Ŭ", "���", 1, 381, 441));
        list_item.Add(new Item(283, "����", "���", 1, 379, 377));
        list_item.Add(new Item(284, "����ź", "���", 1, 384, 443));
        list_item.Add(new Item(285, "ȭ����", "���", 1, 385, 436));
        list_item.Add(new Item(286, "���κ�", "���", 1, 386, 369));
        list_item.Add(new Item(287, "��Ʈ", "���", 1, 396, 412));
        list_item.Add(new Item(288, "��Ƽ�� ī��", "���", 1, 388, 369));
        list_item.Add(new Item(289, "ǥâ", "���", 1, 387, 447));
        list_item.Add(new Item(290, "���", "���", 1, 387, 419));
        list_item.Add(new Item(291, "���", "���", 1, 390, 447));
        list_item.Add(new Item(292, "���", "���", 1, 390, 429));
        list_item.Add(new Item(293, "���", "���", 1, 391, 447));
        list_item.Add(new Item(294, "ũ�ν�����", "���", 1, 391, 379));
        list_item.Add(new Item(295, "�ű׳�-���̽�", "���", 1, 392, 436));
        list_item.Add(new Item(296, "����Ÿ M92F", "���", 1, 392, 427));
        list_item.Add(new Item(297, "STG-44", "���", 1, 393, 443));
        list_item.Add(new Item(298, "�������ʵ�", "���", 1, 394, 432));
        list_item.Add(new Item(299, "����ũ", "���", 1, 395, 430));
        list_item.Add(new Item(300, "�����Ǿ�", "���", 1, 396, 441));
        list_item.Add(new Item(301, "��� �긴��", "���", 1, 397, 355));
        list_item.Add(new Item(302, "�̱� �Ⱦ�", "���", 1, 397, 349));
        list_item.Add(new Item(303, "ī�޶� ��", "���", 1, 398, 392));
        list_item.Add(new Item(304, "����", "���", 1, 399, 412));
        list_item.Add(new Item(305, "�Ӹ���", "���", 1, 399, 377));
        list_item.Add(new Item(306, "������", "���", 1, 400, 368));
        list_item.Add(new Item(307, "�罽 ������", "���", 1, 400, 395));
        list_item.Add(new Item(308, "������", "���", 1, 401, 383));
        list_item.Add(new Item(309, "���� ����", "���", 1, 405, 427));
        list_item.Add(new Item(310, "���� ����", "���", 1, 402, 427));
        list_item.Add(new Item(311, "�ź� ����", "���", 1, 403, 428));
        list_item.Add(new Item(312, "����", "���", 1, 402, 377));
        list_item.Add(new Item(313, "���� �κ�", "���", 1, 403, 407));
        list_item.Add(new Item(314, "�巹��", "���", 1, 437, 368));
        list_item.Add(new Item(315, "��Ű��", "���", 1, 404, 368));
        list_item.Add(new Item(316, "�����", "���", 1, 404, 429));
        list_item.Add(new Item(317, "������", "���", 1, 419, 437));
        list_item.Add(new Item(318, "���� ����", "���", 1, 428, 427));
        list_item.Add(new Item(319, "�д��� ����", "���", 1, 396, 407));
        list_item.Add(new Item(320, "�극�̼�", "���", 1, 407, 427));
        list_item.Add(new Item(321, "���峭 �ð�", "���", 1, 406, 439));
        list_item.Add(new Item(322, "���� ��ȣ��", "���", 1, 411, 439));
        list_item.Add(new Item(323, "ü�� ���뽺", "���", 1, 411, 395));
        list_item.Add(new Item(324, "������", "���", 1, 409, 430));
        list_item.Add(new Item(325, "������", "���", 1, 410, 384));
        list_item.Add(new Item(326, "������", "���", 1, 377, 445));
        list_item.Add(new Item(327, "���� ������", "���", 1, 409, 437));
        list_item.Add(new Item(328, "����", "���", 1, 410, 347));
        list_item.Add(new Item(329, "���ȭ", "���", 1, 326, 383));
        list_item.Add(new Item(330, "������ ����", "���", 1, 419, 418));
        list_item.Add(new Item(331, "����� ��", "���", 1, 413, 388));
        list_item.Add(new Item(332, "���� ����", "���", 1, 385, 383));
        list_item.Add(new Item(333, "����", "���", 1, 414, 437));
        list_item.Add(new Item(334, "���� ������", "���", 1, 432, 420));
        list_item.Add(new Item(335, "���Ż縮", "���", 1, 416, 403));
        list_item.Add(new Item(336, "ȭ����", "���", 1, 427, 379));
        list_item.Add(new Item(337, "�������̰�", "���", 1, 378, 412));
        list_item.Add(new Item(338, "����", "���", 1, 415, 426));
        list_item.Add(new Item(339, "���Ĵܵ�", "���", 1, 371, 433));
        list_item.Add(new Item(340, "ĳ����� �����", "���", 1, 392, 414));
        list_item.Add(new Item(341, "��� ����", "���", 1, 391, 393));
        list_item.Add(new Item(342, "����", "���", 2, 421, 413));
        list_item.Add(new Item(343, "�߰ſ� ��", "���", 3, 423, 431));
        list_item.Add(new Item(344, "����", "���", 1, 435, 385));
        list_item.Add(new Item(345, "�ݶ�", "���", 3, 425, 422));
        list_item.Add(new Item(346, "��ö", "���", 2, 430, 441));
        list_item.Add(new Item(347, "�⸧���� õ", "���", 2, 436, 407));
        list_item.Add(new Item(348, "�߰ſ� ����", "���", 3, 436, 431));
        list_item.Add(new Item(349, "���� ����", "���", 1, 434, 423));
        list_item.Add(new Item(350, "��� ����", "���", 1, 389, 383));
        list_item.Add(new Item(351, "��", "���", 1, 440, 431));
        list_item.Add(new Item(352, "���� ��ǰ", "���", 1, 434, 447));
        list_item.Add(new Item(353, "������ ����", "���", 1, 369, 440));
        list_item.Add(new Item(354, "ö��", "���", 2, 430, 373));
        list_item.Add(new Item(355, "Ȳ��", "���", 1, 374, 438));
        list_item.Add(new Item(356, "�ޱ��� ������", "���", 3, 383, 431));
        list_item.Add(new Item(357, "ö��", "���", 1, 447, 373));
        list_item.Add(new Item(358, "���", "���", 1, 373, 438));
        list_item.Add(new Item(359, "�ϵ�Ŀ��", "���", 1, 440, 427));
        list_item.Add(new Item(360, "���� ����", "���", 2, 446, 426));
        list_item.Add(new Item(361, "������ �㵣", "���", 2, 446, 441));
        list_item.Add(new Item(362, "���̳ʸ���Ʈ", "���", 1, 447, 443));
        list_item.Add(new Item(363, "�Ҷ� �߻���", "���", 3, 442, 384));
        list_item.Add(new Item(364, "���� ī�޶�", "���", 3, 444, 420));
        list_item.Add(new Item(365, "���� ���", "���", 3, 444, 443));
        list_item.Add(new Item(366, "���� ī�޶�", "���", 1, 444, 377));
        list_item.Add(new Item(367, "���� Ʈ��", "���", 1, 446, 443));
        list_item.Add(new Item(368, "����", "���", 1, -1, -1));
        list_item.Add(new Item(369, "������", "���", 1, -1, -1));
        list_item.Add(new Item(370, "��Į", "���", 1, -1, -1));
        list_item.Add(new Item(371, "�콼 ��", "���", 1, -1, -1));
        list_item.Add(new Item(372, "��Į", "���", 1, 369, 370));
        list_item.Add(new Item(373, "��ġ", "���", 1, -1, -1));
        list_item.Add(new Item(374, "���", "���", 1, -1, -1));
        list_item.Add(new Item(375, "�յ���", "���", 1, -1, -1));
        list_item.Add(new Item(376, "��â", "���", 1, -1, -1));
        list_item.Add(new Item(377, "��������", "���", 1, -1, -1));
        list_item.Add(new Item(378, "�ܺ�", "���", 1, -1, -1));
        list_item.Add(new Item(379, "�볪��", "���", 1, -1, -1));
        list_item.Add(new Item(380, "ä��", "���", 1, -1, -1));
        list_item.Add(new Item(381, "��Ŭ", "���", 1, -1, -1));
        list_item.Add(new Item(382, "���尩", "���", 1, -1, -1));
        list_item.Add(new Item(383, "������", "���", 2, -1, -1));
        list_item.Add(new Item(384, "�豸��", "���", 1, -1, -1));
        list_item.Add(new Item(385, "������", "���", 2, -1, -1));
        list_item.Add(new Item(386, "�߱���", "���", 1, -1, -1));
        list_item.Add(new Item(387, "�鵵Į", "���", 1, -1, -1));
        list_item.Add(new Item(388, "Ʈ���� ī��", "���", 1, -1, -1));
        list_item.Add(new Item(389, "����", "���", 1, -1, -1));
        list_item.Add(new Item(390, "���", "���", 1, -1, -1));
        list_item.Add(new Item(391, "����", "���", 1, -1, -1));
        list_item.Add(new Item(392, "���� PPK", "���", 1, -1, -1));
        list_item.Add(new Item(393, "�䵵���� �ڵ�����", "���", 1, -1, -1));
        list_item.Add(new Item(394, "ȭ����", "���", 1, -1, -1));
        list_item.Add(new Item(395, "��罽", "���", 1, -1, -1));
        list_item.Add(new Item(396, "�ٴ�", "���", 1, -1, -1));
        list_item.Add(new Item(397, "������ ��Ÿ", "���", 1, 379, 447));
        list_item.Add(new Item(398, "����", "���", 1, 385, 444));
        list_item.Add(new Item(399, "�Ӹ���", "���", 1, -1, -1));
        list_item.Add(new Item(400, "����", "���", 1, -1, -1));
        list_item.Add(new Item(401, "������ ���", "���", 1, -1, -1));
        list_item.Add(new Item(402, "�ٶ�����", "���", 1, -1, -1));
        list_item.Add(new Item(403, "�º�", "���", 1, -1, -1));
        list_item.Add(new Item(404, "���� ������", "���", 1, -1, -1));
        list_item.Add(new Item(405, "õ ����", "���", 1, -1, -1));
        list_item.Add(new Item(406, "�ո�ð�", "���", 1, -1, -1));
        list_item.Add(new Item(407, "�ش�", "���", 1, -1, -1));
        list_item.Add(new Item(408, "����", "���", 1, -1, -1));
        list_item.Add(new Item(409, "������", "���", 1, -1, -1));
        list_item.Add(new Item(410, "�ȭ", "���", 1, -1, -1));
        list_item.Add(new Item(411, "Ÿ����", "���", 1, -1, -1));
        list_item.Add(new Item(412, "����", "���", 1, -1, -1));
        list_item.Add(new Item(413, "��", "���", 1, -1, -1));
        list_item.Add(new Item(414, "����", "���", 1, -1, -1));
        list_item.Add(new Item(415, "��ä", "���", 1, -1, -1));
        list_item.Add(new Item(416, "�Ұ�", "���", 1, -1, -1));
        list_item.Add(new Item(417, "����", "���", 1, -1, -1));
        list_item.Add(new Item(418, "����", "���", 1, -1, -1));
        list_item.Add(new Item(419, "���ڰ�", "���", 1, -1, -1));
        list_item.Add(new Item(420, "�־Ȱ�", "���", 1, -1, -1));
        list_item.Add(new Item(421, "����", "���", 1, -1, -1));
        list_item.Add(new Item(422, "��", "���", 1, -1, -1));
        list_item.Add(new Item(423, "��", "���", 3, -1, -1));
        list_item.Add(new Item(424, "����", "���", 2, -1, -1));
        list_item.Add(new Item(425, "ź���", "���", 1, -1, -1));
        list_item.Add(new Item(426, "��", "���", 1, -1, -1));
        list_item.Add(new Item(427, "����", "���", 1, -1, -1));
        list_item.Add(new Item(428, "�ź��� �����", "���", 1, -1, -1));
        list_item.Add(new Item(429, "��", "���", 1, -1, -1));
        list_item.Add(new Item(430, "��ö", "���", 1, -1, -1));
        list_item.Add(new Item(431, "������", "���", 1, -1, -1));
        list_item.Add(new Item(432, "������ ������", "���", 1, -1, -1));
        list_item.Add(new Item(433, "����", "���", 1, -1, -1));
        list_item.Add(new Item(434, "���͸�", "���", 1, -1, -1));
        list_item.Add(new Item(435, "���ڿ�", "���", 1, -1, -1));
        list_item.Add(new Item(436, "����", "���", 1, -1, -1));
        list_item.Add(new Item(437, "�ʰ�", "���", 1, -1, -1));
        list_item.Add(new Item(438, "����", "���", 1, -1, -1));
        list_item.Add(new Item(439, "������", "���", 1, -1, -1));
        list_item.Add(new Item(440, "����", "���", 1, -1, -1));
        list_item.Add(new Item(441, "ö����", "���", 3, -1, -1));
        list_item.Add(new Item(442, "ĵ", "���", 1, -1, -1));
        list_item.Add(new Item(443, "ȭ��", "���", 1, -1, -1));
        list_item.Add(new Item(444, "���� ī�޶�", "���", 1, -1, -1));
        list_item.Add(new Item(445, "�ð���", "���", 1, -1, -1));
        list_item.Add(new Item(446, "�㵣", "���", 2, -1, -1));
        list_item.Add(new Item(447, "�ǾƳ뼱", "���", 1, -1, -1));

        #endregion
    }

    void Start()
    {
        RouteAlgorithm();
    }

    void Update()
    {
        Route_in_Sequence();
        RouteInfo();
    }

    #region UI ��Ʈ�ѷ�

    void OnBtnClick(int MapNumber)
    {
        if (!isSelected[MapNumber])
        {
            list_mapNum.Add(MapNumber);
            isSelected[MapNumber] = true;
        }
        else
        {
            list_mapNum.Remove(MapNumber);
            isSelected[MapNumber] = false;
        }

        RouteAlgorithm();
    } // ��Ʈ ���� ���

    void OnMouseEnter(int area)
    {
        List<string> list_s = new List<string>();
        string str = "";

        str = string.Format("[{0}] ", list_mapName[area]);
        list_s.Add(str);

        foreach (int element in list_map[area])
        {
            if (list_itemGuide.Contains(element))
            {
                str = string.Format("({0}) ", list_item[element].name);
                list_s.Add(str);
            }
        }

        str = "";
        foreach (string element in list_s)
        {
            str = string.Format("{0}{1}", str, element);
        }

        txtMapMaterial.text = str;
    } // ���� ��� ǥ��

    void OnMouseExit()
    {
        txtMapMaterial.text = "";
    }

    void Route_in_Sequence()
    {
        for (int i = 0; i < 16; i++)
        {
            string str1 = "";
            txtMapNum[i].text = list_mapName[i];

            for (int j = 0; j < list_mapNum.Count; j++)
            {
                if (i == list_mapNum[j])
                {
                    str1 = string.Format("{0}", j + 1);
                }
            }

            string str2 = string.Format("{0} {1}", str1, txtMapNum[i].text);
            txtMapNum[i].text = str2;
        }
    } // ��Ʈ ���� ǥ��

    void RouteInfo()
    {
        string str = "";

        foreach (string element in list_RouteInfo)
        {
            str = string.Format("{0}{1}", str, element);
            txtRouteInfo.text = str;
        }
    } // ��Ʈ ���� ǥ��

    #endregion

    public void RouteAlgorithm()
    {
        list_inventory.Clear();
        list_itemGuide.Clear();
        list_RouteInfo.Clear();

        for (int i = 0; i < itemSlot.Length; i++)
        {
            list_item[itemSlot[i]].GetRecipe(); // ��ǥ ������ ��� ȣ��
            list_itemGuide.Add(itemSlot[i]);
        }

        list_inventory.Clear();

        #region ���� ���� ����

        switch (weapon)
        {
            case StartWeapon.DAGGER:
                GetItem(370); // ��Į
                break;

            case StartWeapon.TWOHANDEDSWORD:
                GetItem(371); // �콼 ��
                break;

            case StartWeapon.AXE:
                GetItem(375); // �յ���
                break;

            case StartWeapon.DUALSWORDS:
                GetItem(372); // �ְ�
                break;

            case StartWeapon.PISTOL:
                GetItem(392); // ���� PPK
                break;

            case StartWeapon.ASSAULTRIFLE:
                GetItem(393); // �䵵���� �ڵ�����
                break;

            case StartWeapon.SNIPERRIFLE:
                GetItem(394); // ȭ����
                break;

            case StartWeapon.RAPIER:
                GetItem(396); // �ٴ�
                break;

            case StartWeapon.SPEAR:
                GetItem(376); // ��â
                break;

            case StartWeapon.HAMMER:
                GetItem(373); // ��ġ
                break;

            case StartWeapon.BAT:
                GetItem(378); // �ܺ�
                break;

            case StartWeapon.THROW:
                GetItem(386); // �߱���
                break;

            case StartWeapon.SHURIKEN:
                GetItem(387); // �鵵Į
                break;

            case StartWeapon.BOW:
                GetItem(390); // ???��
                break;

            case StartWeapon.CROSSBOW:
                GetItem(391); /// ����
                break;

            case StartWeapon.GLOVE:
                GetItem(382); // ���尩
                break;

            case StartWeapon.TONFA:
                GetItem(379); // �볪��
                break;

            case StartWeapon.GUITAR:
                GetItem(397); // ������ ��Ÿ
                break;

            case StartWeapon.NUNCHAKU:
                GetItem(395); // ��罽
                break;

            case StartWeapon.WHIP:
                GetItem(380); // ä��
                break;

            case StartWeapon.CAMERA:
                GetItem(398); // ����
                break;
        }

        #endregion

        GetItem(423, 2); // ���� ������ �� 2�� �߰�

        for (int i = 0; i < list_mapNum.Count; i++)
        {
            list_RouteInfo.Add(string.Format("[ {0} ]\n", list_mapName[list_mapNum[i]]));

            list_RouteInfo.Add("= ȹ�� ���� =\n");
            GetMaterials(list_mapNum[i]); //  ���� �� ��� ȹ��

            list_RouteInfo.Add("= ���� ���� =\n");
            Search(list_mapNum[i]); // ������ ��ᰡ �𿴴��� Ȯ��

            list_RouteInfo.Add("\n");
        }

        list_RouteInfo.Add("[ �ʿ� ��� ]\n");

        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int count = 0;
            int temp = list_itemGuide[i];

            for (int j = 0; j < list_itemGuide.Count; j++)
            {
                if (list_itemGuide[j] == temp)
                {
                    count++;
                }
            }

            InputRouteInfo(temp, count, 2);
        }
    }

    #region ��Ʈ �˰���

    void GetItem(int index, int quantity = 1) // ȹ�� �Լ�
    {
        for (int i = 0; i < quantity; i++)
        {
            list_inventory.Add(index);

            if (list_itemGuide.Contains(index))
            {
                list_itemGuide.Remove(index);
            }
        }
    }

    void GetMaterials(int area) // ��� ���� �Լ�
    {
        foreach (int materials in list_map[area])
        {
            int count = 0;

            if (list_itemGuide.Contains(materials))
            {
                for (int i = 0; i < list_itemGuide.Count; i++)
                {
                    if (list_itemGuide[i] == materials)
                    {
                        count++;

                        GetItem(materials, list_item[materials].quantity);
                    }
                }

                InputRouteInfo(materials, count * list_item[materials].quantity, 0);

                GetMaterials(area);
            }
        }
    }

    void Search(int area) // ��� ���� Ȯ�� �Լ�
    {
        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int temp = list_itemGuide[i];
            bool[] isHave = new bool[2] { false, false };

            for (int j = 0; j < list_item[temp].recipe.Length; j++)
            {
                if (list_inventory.Contains(list_item[temp].recipe[j]))
                {
                    isHave[j] = true;
                }
            }

            if (isHave[0] && isHave[1])
            {
                Craft(temp, area);
                i--;
            }
        }
    }

    void Craft(int index, int area) // ���� �Լ�
    {
        Item tempItem = list_item[index];

        for (int i = 0; i < tempItem.recipe.Length; i++)
        {
            int tempInt = tempItem.recipe[i];
            list_inventory.Remove(tempInt);
        }

        if(list_item[index].type == "���")
        {
            InputRouteInfo(index, tempItem.quantity, 1);
        }

        GetItem(index, tempItem.quantity); // ������ ����
    }

    #endregion

    void InputRouteInfo(int index, int count, int type) // 0 : �з� ���� 1 : ��� 2 : �����
    {
        string str = "";
        Item temp = list_item[index];

        switch (type)
        {
            case 0:
                str = string.Format("({0} {1}��)\n", temp.name, count);
                list_RouteInfo.Add(str);
                break;
            case 1:
                str = string.Format("({0})\n", temp.name);
                list_RouteInfo.Add(str);
                break;
            case 2:
                str = string.Format("({0} {1}��)\n", temp.name, count);
                if (temp.recipe[0] == -1 && temp.recipe[1] == -1 && !list_RouteInfo.Contains(str))
                {
                    list_RouteInfo.Add(str);
                }
                break;
        }
    }

    #region �����

    public void CheckListMap()
    {
        for (int i = 0; i < 16; i++)
        {
            foreach (int value in list_map[i])
            {
                string str = string.Format("[{0}] {1}��° ������: {1}",list_mapName[i]  ,i + 1, value);
                Debug.Log(str);
            }
        }
    }

    public void CheckListItem()
    {
        foreach (Item value in list_item)
        {
            Debug.Log(value.name);
        }
    }

    public void CheckItemGuide()
    {
        for (int i = 0; i < list_itemGuide.Count; i++)
        {
            int temp = list_itemGuide[i];
            string str = string.Format("{0} ��° ��ǥ ������: {1}", i, list_item[temp].name);
            Debug.Log(str);
        }
    }

    public void CheckInventory()
    {
        for (int i = 0; i < list_inventory.Count; i++)
        {
            int temp = list_inventory[i];
            string str = string.Format("{0} ��° �κ��丮 ������: {1}", i, list_item[temp].name);
            Debug.Log(str);
        }
    }

    public void DebugLogClear()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #endregion
}

public class Item
{
    public int index;
    public string name;
    public string type; // "���" "���"
    public int quantity;
    public int[] recipe = new int[2];

    public Item() { }
    public Item(int Index, string Name, string Type, int Quantity, int L_Node, int R_Node)
    {
        index = Index;
        name = Name;
        type = Type;
        quantity = Quantity;
        recipe[0] = L_Node;
        recipe[1] = R_Node;
    }

    public void GetRecipe() // ��� ���� �� ������
    {
        for (int i = 0; i < recipe.Length; i++)
        {
            if (recipe[i] != -1)
            {
                Route.list_item[recipe[i]].GetRecipe();
            }
        }

        if (Route.list_inventory.Contains(index))
        {
            Route.list_inventory.Remove(index);
            return; // �κ��丮�� �̹� ���� �������� ���� ��� ������ ����
        }

        for (int i = 0; i < quantity - 1; i++)
        {
            Route.list_inventory.Add(index); // ������� ���� ���� ������ ȹ��
        }

        if(recipe[0] != -1 || recipe[1] != -1)
        {
            for (int i = 0; i < recipe.Length; i++)
            {
                Route.list_itemGuide.Add(recipe[i]); // ���� ���� ��� �������� ����Ʈ�� �߰�
            }
        }
    }
}