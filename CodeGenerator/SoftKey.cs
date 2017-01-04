using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CodeGenerator
{
	/// <summary>
	/// Class1 ��ժҪ˵����
	/// </summary>
	
	public class SoftKey
	{
		[DllImport("kernel32.dll")]
		public static extern  int lstrcmp(byte []  pDest , byte [] pSource);
		[DllImport("kernel32.dll")]
		public static extern  int lstrcmpi(byte [] pDest, byte [] pSource);
		[DllImport("kernel32.dll")] 
		public static extern  int lstrcat(StringBuilder  pDest , byte [] pSource );
		[DllImport("kernel32.dll",  EntryPoint="lstrcat")]
		public static extern  int lstrcat_2(StringBuilder  pDest , string pSource );
		[DllImport("kernel32.dll")]
		public static extern  int lstrcpy(StringBuilder  pDest , String pSource);
		[DllImport("kernel32.dll",  EntryPoint="RtlMoveMemory")]
		public static extern  void  CopyLong(ref int pDest , ref float pSource , int ByteLen );

		[DllImport ("YtSmart.dll")]
		public static extern  short FindPort(int start, StringBuilder  sKeyPath);
		[DllImport ("YtSmart.dll")]
		public static extern  short FindPort_2(int start, byte [] InByte,int InLen, StringBuilder  sKeyPath);
		[DllImport ("YtSmart.dll")]
		public static extern  short CalEx(byte [] InByte,int in_len,ref int D0,ref int D1,ref int D2,ref int D3,ref int D4,ref int D5,ref int D6,ref int D7,
			ref float F0,ref float F1,ref float F2,ref float F3,ref float F4,ref float F5,ref float F6,ref float F7,
			StringBuilder  S0,StringBuilder  S1,StringBuilder  S2,StringBuilder  S3,StringBuilder  S4,StringBuilder  S5,StringBuilder  S6,StringBuilder  S7,String  sKeyPath,int over_count);
		[DllImport ("YtSmart.dll")]
		public static extern  short  GetIDVersion(ref  uint ID,ref short version,String KeyPath);
		public   String  KeyPath; 
		public bool b_ini ;//�Ƿ��ѽ���ʼ������
		public int LastError;//�����������
		public SoftKey()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//���º������ڽ��ֽ�����ת��Ϊ���ַ���
		public static string ByteConvertString(byte[] buffer) 
		{
			char []null_string={'\0','\0'};
			System.Text.Encoding encoding = System.Text.Encoding.Default;
			return encoding.GetString(buffer).TrimEnd(null_string);
		}
		//���º������ڽ����ַ���ת��Ϊ�ֽ�����
		public static bool StringConvertByte(byte[] buffer,string InS) 
		{
			byte []temp;int n;
			temp=System.Text.Encoding.Default.GetBytes(InS); 
			if (temp.Length>50)return false;
			for (n=1;n<=temp.Length;n++)
			{
				buffer[n-1]=temp[n-1];
			}
			return true;
		}

		//���º������ڽ����ַ���ת��Ϊ�ֽ�����
		public static bool StringConvertByteEx(byte[] buffer,string InS) 
		{
			byte []temp;int n;
			temp=System.Text.Encoding.Default.GetBytes(InS); 
			for (n=1;n<=temp.Length;n++)
			{
				buffer[n-1]=temp[n-1];
			}
			return true;
		}

		public static void StringToDword(ref int outd0,ref int outd1,ref int outd2,byte[] buffer,int start_pos)
		{
			int n; uint d0=0,d1=0,d2=0;int temp_len;
			int len=buffer.Length-start_pos;
			if(len>4)temp_len=4;else temp_len=len;
			for (n=temp_len;n>0;n--)
			{
				d0=d0 | (uint)((buffer[n-1+4*0+start_pos]&255)<<((temp_len-n)*8));
			}
			len=len-4;if(len<0)goto my_exit;
			if(len>4)temp_len=4;else temp_len=len;
			for (n=temp_len;n>0;n--)
			{
				d1=d1 | (uint)((buffer[n-1+4*1+start_pos]&255)<<((temp_len-n)*8));
			}
			len=len-4;if(len<0)goto my_exit;
			if(len>4)temp_len=4;else temp_len=len;
			for (n=temp_len;n>0;n--)
			{
				d2=d2 | (uint)((buffer[n-1+4*2+start_pos]&255)<<((temp_len-n)*8));
			}
			my_exit:
				outd0=(int)d0;outd1=(int)d1;outd2=(int)d2;
				return ;
		}

		//�ڳ���ʼ����ʱ�����ȵ���Ini���̣����Ҷ�Ӧ�ļ��������ڵ��豸·����
		//����ҵ���Ӧ�ļ��������Ὣ��·�������ڱ���KeyPath�У�
		//�Ա����������ĵ��ã�
		 public short Ini() 
		{

			//���´����ǲ��Ҷ�Ӧ�ļ��������ڵ��豸·�����ô����ɿ��������Զ����ɣ�
			 byte [] EncByte={24,0,1,101,73,197,201,221,140,137,143,202,225,210,254,211,110,232,183,194,20,217,75,207,53,121};
			StringBuilder sKeyPath = new StringBuilder("", 260);
			//���ɸ�����ı��ʽΪ��D0=123,D1=123,D2=123,D3=123������ϵͳ�п��ܴ��ڶ�Ѽ����������ڲ��Ҷ�Ӧ�ļ�����
			{
				short ret= FindPort_2(0,EncByte,26,sKeyPath);
				KeyPath = sKeyPath.ToString();
				return ret;
				
			}
			
		}

		//�������YCheckKey
//Ŀ�ģ������Լ���Ƿ���ڶ�Ӧ�ļ�����
//���ؽ����Ϊ���ʾ���ڶ�Ӧ�ļ�����������Ϊ��
//���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�

 public bool YCheckKey()
{
	int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;
	int pc_d0,pc_d1,pc_d2,pc_d3;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
   //ԭ�������������Ȼ���ڳ���˶���������м������㣬Ȼ���ü����������������н������㣬�����ܺ�Ľ���Ƿ������ǰ�������������������Ϊ���ڶ�Ӧ�ļ�����
   //����ǿ�ȵͣ����������������ڲ����ã������ж��Ƿ���ڶ�Ӧ������
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return false;}//���û�н��г�ʼ�������Ƚ��г�ʼ������
	//�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
	//ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
	//�����ڳ��������Ӷ�YCheckKeyArray������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
	 byte [] EncByte={57,1,161,189,19,141,88,2,214,215,172,98,13,65,204,160,184,102,242,44,33,174,128,36,165,31,38,204,112,246,36,198,221,144,8,9,229,214,79,195,213,167,59,197,143,204,68,135,8,26,
84,132,106,134,239,84,39,74,239,51,14,187,155,190,49,131,246,250,88,50,198,118,39,192,112,151,120,175,162,98,127,216,191,110,44,186,238,69,228,144,132,57,197,2,219,16,184,102,223,190,
8,153,212,28,87,191,38,204,112,246,36,198,221,144,8,9,229,214,79,195,213,167,106,75,159,47,20,239,160,6,124,164,224,214,230,250,198,191,26,36,66,215,39,88,114,132,185,213,226,8,
242,86,117,249,180,170,72,167,122,32,90,78,215,31,126,4,1,99,75,90,79,111,103,53,254,161,150,53,205,105,75,135,73,80,209,249,110,242,221,240,216,234,158,97,8,9,229,214,79,195,
213,167,59,197,143,204,68,135,8,26,84,132,106,134,239,84,39,74,205,233,64,150,145,144,85,91,155,150,185,41,226,224,39,234,77,167,26,168,141,115,182,226,155,172,10,242,148,187,235,171,
130,125,193,185,193,130,89,55,156,125,121,250,24,255,192,109,249,150,65,246,229,218,29,213,106,75,159,47,20,239,160,6,124,164,224,214,230,250,198,191,186,87,164,130,59,244,96,232,130,125,
193,185,193,130,89,55,156,125,121,250,24,255,192,109,67,61,2,142,2,219,6,172};
	 
	//////////////////////////////////////////////////////////////////////////////////////
	//���������
	System.Random rnd=new System.Random();
	pc_d0=d0 = rnd.Next(0,2147483646)  ;pc_d1=d1 =rnd.Next(0,2147483646); pc_d2=d2 = rnd.Next(0,2147483646);pc_d3=d3 = rnd.Next(0,2147483646);
	//�����ڳ���˶���������м�������
	d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d0= (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
d2= d2 ^  467721599;
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d2= (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
d2= d2 ^ d3;
d3= (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
d0= (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
d0= d0 ^ d2;
d3= d3 ^ d1;
d3= d3 ^  1880722559;
d0= (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
d1= d1 ^ d0;
d0= (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
d0= (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d3= (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
d3= (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
d0= (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));

	//���½�������͵�������������������
	
	{
		LastError=CalEx(EncByte, 322,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return false ; }
	//�����ͬ������ڶ�Ӧ�����������򣬲����ڶ�Ӧ�ļ�����
	if ((d0 == pc_d0) && (d1 == pc_d1) && (d2 == pc_d2) && (d3 == pc_d3))
	{
		return true;
	}
	return false;
}


//�������Ystrcpy
//Ŀ�ģ����ַ������и���
//����sr:Դ�ַ���
//���ؽ�������ؽ���ַ���
//���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�



 public string Ystrcpy(string sr)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
    //ԭ���ȶ�Ҫ��������Ĳ����ڳ���˽��м������㣬Ȼ�������жԸò������н������㣬Ȼ���������н���������ԭ��Ȼ�󷵻ؽ���������

   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return "";}//���û�н��г�ʼ�������Ƚ��г�ʼ������
	//�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
	//ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
	//�����ڳ��������Ӷ�YstrcpyArray������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
	 byte [] EncByte={115,1,185,213,226,8,242,86,117,249,180,170,72,167,122,32,90,78,186,87,164,130,59,244,96,232,130,125,193,185,193,130,89,55,156,125,121,250,24,255,192,109,8,117,224,157,202,115,70,124,
190,19,150,176,82,200,134,176,226,181,205,106,153,99,108,72,154,116,92,92,201,139,221,125,237,255,112,195,238,224,228,43,120,30,42,10,175,38,124,231,55,254,33,176,142,25,89,130,130,168,
228,134,50,69,13,226,59,197,143,204,68,135,8,26,84,132,106,134,239,84,39,74,51,39,183,54,109,193,36,143,5,242,240,146,86,246,162,34,222,216,110,85,21,184,241,232,8,117,224,157,
202,115,70,124,190,19,150,176,82,200,134,176,125,86,79,36,149,128,155,142,55,254,33,176,142,25,89,130,130,168,228,134,50,69,13,226,154,116,92,92,201,139,221,125,237,255,112,195,238,224,
228,43,229,201,103,194,157,146,167,33,130,250,245,67,163,67,50,105,94,139,33,69,227,7,200,67,130,168,228,134,50,69,13,226,207,32,48,156,143,83,222,229,220,188,215,100,29,1,233,10,
140,130,141,18,32,214,9,162,210,61,101,81,26,28,99,121,222,216,110,85,21,184,241,232,101,157,224,70,112,147,47,169,175,126,152,93,17,50,243,217,21,26,118,44,158,248,26,245,207,32,
48,156,143,83,222,229,220,188,215,100,29,1,233,10,153,120,61,126,114,104,192,147,200,117,152,217,129,61,135,33,10,186,152,93,251,216,11,231,65,47,170,197,79,161,128,249,52,133,146,99,
154,116,58,120,91,237,96,203,164,17,205,127,99,8,233,125,51,143,201,66,220,214,84,214,188,103,99,150};
	//////////////////////////////////////////////////////////////////////////////////////
	//��Ҫ�������ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d0-d2,���ڼ����Ժ�ļ�������
	byte [] temp_string=System.Text.Encoding.Default.GetBytes(sr); int nLen=temp_string.Length;
	byte [] leave_string=new byte[nLen];
	if (nLen>0){StringToDword(ref d0,ref d1,ref d2,temp_string,0);leave_string[0] = 0;}
	//�ٷֽ�һ�Σ���ֵ��d3-d5
	if(nLen>12)StringToDword(ref d3,ref d4,ref d5,temp_string,12);
	//������µ��ַ���
    int temp_len = nLen - 24;
    for(n=0;n<temp_len;n++)leave_string[n] = temp_string[24 + n];
	//�����ڳ���˶�����������м�������
	d3= d3 ^ d2;
d1= d1 ^ d2;
d4= (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
d4= (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
d4= d4 ^ d3;
d2= (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
d4= (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
d5= (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
d0= d0 ^  1359305215;
d5= (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
d5= (int)((((uint)d5) >> 1) | (((uint)d5) << 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d2= (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
d3= (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
d5= (int)((((uint)d5) >> 1) | (((uint)d5) << 31));
d5= (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
d5= d5 ^ d3;
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d0= (int)((((uint)d0) << 1) | (((uint)d0) >> 31));

	//���½����ܺ�Ĳ����͵������������������㣬�������ܺ�Ľ���ϲ�Ϊ�ַ�����
	
	{
		LastError=CalEx(EncByte, 378,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return "" ; }
	//���û�з��ش����򽫽��ܲ�����Ľ������;
	
	//������ʱĿ�Ļ�����
	StringBuilder Temp_byte=new StringBuilder("",nLen);
	//Ȼ�󽫽���ַ������Ƶ�Ŀ�ĵ��ַ�����
	if(nLen>0)
	{
		{
			lstrcpy (Temp_byte, s0.ToString());
			//�ٽ��ֽ�ʣ��ĵ��ַ������ӵ�Ŀ���ַ�����
			lstrcat (Temp_byte, leave_string);
		}
	}
	return Temp_byte.ToString();
}


//�������YCompareString
//Ŀ�ģ������������ַ������бȽ�
//����ins1��Ҫ�Ƚϵ������ַ���֮һ
//����ins2��Ҫ�Ƚϵ������ַ���֮һ
//���ؽ�������Ϊ�棬���������ַ�����ȣ�����Ϊ��
//���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�



 public bool YCompareString(string ins1,string ins2)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
    //ԭ���ȶ�Ҫ��������Ĳ����ڳ���˽��м������㣬Ȼ�������жԸò������н������㲢���бȽϣ�Ȼ�󷵻ؽ���������
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return false;}//���û�н��г�ʼ�������Ƚ��г�ʼ������
	//�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
	//ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
	//�����ڳ��������Ӷ�YCompareStringArray������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
	 byte [] EncByte={151,1,161,189,19,141,88,2,214,215,172,98,13,65,204,160,184,102,45,150,149,166,37,11,238,96,165,78,219,189,24,16,160,109,156,125,121,250,24,255,192,109,8,117,224,157,202,115,70,124,
190,19,150,176,82,200,134,176,223,179,240,156,174,215,82,119,175,126,152,93,17,50,243,217,21,26,118,44,158,248,26,245,134,75,80,234,201,130,249,232,161,189,19,141,88,2,214,215,172,98,
13,65,204,160,184,102,172,166,65,240,160,218,248,215,217,137,121,81,45,63,18,36,112,151,120,175,162,98,127,216,103,61,170,130,86,193,62,94,15,148,74,240,59,78,4,16,155,172,10,242,
148,187,235,171,130,125,193,185,193,130,89,55,156,125,121,250,24,255,192,109,191,110,44,186,238,69,228,144,132,57,197,2,219,16,184,102,73,166,64,76,52,246,160,83,106,75,159,47,20,239,
160,6,124,164,224,214,230,250,198,191,111,153,200,205,58,97,6,23,192,215,9,206,254,200,81,75,21,26,118,44,158,248,26,245,161,189,19,141,88,2,214,215,172,98,13,65,204,160,184,102,
113,55,145,239,48,249,242,101,110,242,221,240,216,234,158,97,8,9,229,214,79,195,213,167,131,225,203,225,174,53,117,52,210,61,101,81,26,28,99,121,222,216,110,85,21,184,241,232,161,189,
19,141,88,2,214,215,172,98,13,65,204,160,184,102,45,150,149,166,37,11,238,96,165,78,219,189,24,16,160,109,156,125,121,250,24,255,192,109,103,238,230,227,178,171,210,156,243,108,221,12,
60,179,74,137,54,84,73,55,45,150,64,255,41,2,89,137,116,111,158,70,208,229,125,12,155,230,38,10,148,5,71,98,41,182,140,197,242,148,143,132,81,151,120,99,119,134,8,84,96,15,
197,131,185,56,118,147,88,158,16,177};
	//////////////////////////////////////////////////////////////////////////////////////
	//����һ���ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d0-d2,���ڼ����Ժ�ļ�������
	byte [] temp_string_1=System.Text.Encoding.Default.GetBytes(ins1); int nLen_1=temp_string_1.Length;
	byte [] leave_string_1=new byte[nLen_1];
	if(nLen_1>0){StringToDword(ref d0,ref d1,ref d2,temp_string_1,0);leave_string_1[0] = 0;}
	//������µ��ַ���
    int temp_len = nLen_1 - 12;
    for(n=0;n<temp_len;n++)leave_string_1[n] = temp_string_1[12 + n];
	//���ڶ����ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d3-d5,���ڼ����Ժ�ļ�������
	byte [] temp_string_2=System.Text.Encoding.Default.GetBytes(ins2); int nLen_2=temp_string_2.Length;
	byte [] leave_string_2=new byte[nLen_2];
	if(nLen_2>0){StringToDword(ref d3,ref d4,ref d5,temp_string_2,0);leave_string_2[0] = 0;}
	//������µ��ַ���
	temp_len = nLen_2 - 12;
	for(n=0;n<temp_len;n++)leave_string_2[n] = temp_string_2[12 + n];
	//�����ڳ���˶�����������м�������
	d1= (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d2= (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
d4= d4 ^ d1;
d0= (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d0= (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
d4= d4 ^ d2;
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d2= (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
d5= d5 ^ d2;
d5= d5 ^ d1;
d4= (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d1= (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
d1= (int)((((uint)d1) << 1) | (((uint)d1) >> 31));

	//���½����ܺ�Ĳ����͵������������������㣬�������ܺ�Ľ���ϲ�Ϊ�ַ������ٽ��бȽ�
	
	{
		LastError=CalEx(EncByte, 410,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return false ; }
	//���û�з��ش����򽫽��ܲ�����Ľ������;
	if(d7==0)return false;//���d7��Ϊ0���������ڽ��бȽϵĽ���������ֱ�ӷ��ؼ�
	//������ڱȽ�������ٱȽ����µĲ���
	return (lstrcmp(temp_string_1,temp_string_2)==0);;
}

//�������Ystrcat
//Ŀ�ģ������������ַ�����������
//����ins1��Ҫ���ӵ������ַ���֮һ
//����ins2��Ҫ���ӵ������ַ���֮һ
//���ؽ�����������Ӻ���ַ���
//���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�



 public string Ystrcat(string ins1,string ins2)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
   //ԭ���ȶ�Ҫ��������Ĳ����ڳ���˽��м������㣬Ȼ�������жԸò������н������㣬Ȼ���������н���������ԭ��Ȼ�󷵻ؽ���������
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return "";}//���û�н��г�ʼ�������Ƚ��г�ʼ������
	//�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
	//ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
	//�����ڳ��������Ӷ�YstrcatArray������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
	 byte [] EncByte={115,1,20,191,100,39,113,171,112,205,37,108,170,153,26,138,244,204,80,80,40,181,50,91,198,5,217,137,121,81,45,63,18,36,112,151,120,175,162,98,127,216,185,213,226,8,242,86,117,249,
180,170,72,167,122,32,90,78,111,153,200,205,58,97,6,23,192,215,9,206,254,200,81,75,21,26,118,44,158,248,26,245,155,150,185,41,226,224,39,234,77,167,26,168,141,115,182,226,32,111,
171,32,100,9,196,47,165,78,219,189,24,16,160,109,156,125,121,250,24,255,192,109,191,110,44,186,238,69,228,144,132,57,197,2,219,16,184,102,119,212,115,124,255,36,27,156,94,139,33,69,
227,7,200,67,130,168,228,134,50,69,13,226,8,117,224,157,202,115,70,124,190,19,150,176,82,200,134,176,222,176,215,180,25,46,105,172,5,242,240,146,86,246,162,34,222,216,110,85,21,184,
241,232,8,117,224,157,202,115,70,124,190,19,150,176,82,200,134,176,170,242,38,212,202,46,245,166,217,137,121,81,45,63,18,36,112,151,120,175,162,98,127,216,213,83,123,129,112,13,178,124,
120,230,184,217,241,108,76,11,67,172,186,58,158,175,195,144,215,172,254,65,183,160,8,183,192,215,9,206,254,200,81,75,21,26,118,44,158,248,26,245,173,75,221,28,109,14,14,134,154,116,
92,92,201,139,221,125,237,255,112,195,238,224,228,43,2,45,217,138,64,3,107,126,103,238,230,227,178,171,210,156,243,108,221,12,60,179,74,137,54,84,73,55,45,150,64,255,41,2,89,137,
116,111,158,70,208,229,125,12,155,230,38,10,148,5,71,98,41,182,140,197,105,233,231,228,126,220,1,246};

	//////////////////////////////////////////////////////////////////////////////////////
	//����һ���ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d0-d2,���ڼ����Ժ�ļ�������
	byte [] temp_string_1=System.Text.Encoding.Default.GetBytes(ins1); int nLen_1=temp_string_1.Length;
	byte [] leave_string_1=new byte[nLen_1];
	if(nLen_1>0){StringToDword(ref d0,ref d1,ref d2,temp_string_1,0);leave_string_1[0] = 0;}
	//������µ��ַ���
    int temp_len = nLen_1 - 12;
    for(n=0;n<temp_len;n++)leave_string_1[n] = temp_string_1[12 + n];
	//���ڶ����ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d3-d5,���ڼ����Ժ�ļ�������
	byte [] temp_string_2=System.Text.Encoding.Default.GetBytes(ins2); int nLen_2=temp_string_2.Length;
	byte [] leave_string_2=new byte[nLen_2];
	if(nLen_2>0){StringToDword(ref d3,ref d4,ref d5,temp_string_2,0);leave_string_2[0] = 0;}
	//������µ��ַ���
	temp_len = nLen_2 - 12;
	for(n=0;n<temp_len;n++)leave_string_2[n] = temp_string_2[12 + n];
	//�����ڳ���˶�����������м�������
	d1= d1 ^ d2;
d5= (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
d3= d3 ^ d2;
d3= d3 ^ d1;
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d1= d1 ^ d4;
d1= d1 ^ d2;
d1= (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d2= (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d5= (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d1= (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
d2= (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
d4= (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
d0= (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
d3= (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
d5= (int)((((uint)d5) >> 1) | (((uint)d5) << 31));

	//���½����ܺ�Ĳ����͵������������������㣬�������ܺ�Ľ���ϲ�Ϊ�ַ�����
	
	{
		LastError=CalEx(EncByte, 378,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return "" ; }
	//���û�з��ش����򽫽��ܲ�����Ľ������;
	
	//������ʱĿ�Ļ�����
	StringBuilder Temp_byte=new StringBuilder("",nLen_1+nLen_2);
	//Ȼ�󽫽���ַ������Ƶ�Ŀ�ĵ��ַ�����
	if(nLen_1+nLen_2>0)
	{
		{
			lstrcpy (Temp_byte, s0.ToString());
			//�ٽ��ֽ�ʣ��ĵ��ַ������ӵ�Ŀ���ַ�����
			lstrcat (Temp_byte, leave_string_1);
			//�����ӵڶ����ַ����ļ��ܲ����ܺ�Ĳ���
			lstrcat_2 (Temp_byte, s1.ToString());
			//���������µĲ���
			lstrcat (Temp_byte, leave_string_2);
		}
	}
	return Temp_byte.ToString();
}



		
		 public short GetIDVersionEx(ref uint id,ref short ver) 
		{
			{
				return GetIDVersion(ref id,ref ver,KeyPath);
			}
		}
		
		 public short FindPortEx(int start) 
		{
            StringBuilder sKeyPath = new StringBuilder("", 260);
            {
                short ret = FindPort(start, sKeyPath);
                KeyPath = sKeyPath.ToString();
                return ret;
            }
		}

		public string  GetErrInfo(short err )
		{
			switch(err) 
			{
				case -1:
					return "δ�ҵ����ؽ������";

				case -2:
					return "δ�ҵ� = ����";
					
				case -3:
					return "����û���ҵ���Ӧ����";
					
				case -5:
					return "�����Ҳ����ַ����ĵ�һ��˫����";
					
				case -6:
					return "�����Ҳ����ַ����ĵڶ���˫����";
					
				case -7:
					return "IF���û���ҵ�goto�ַ�";
					
				case -8:
					return "IF���û���ҵ���һ���Ƚ��ַ�";
					
				case -9:
					return "IF���û���ҵ��ȽϷ���";
					
				case -10:
					return "���߱������Ͳ����";
					
				case -11:
					return "û���ҵ�NOT����";
					
				case -12:
					return "�������α���";
					
				case -13:
					return "����û���ҵ���Ӧ���γ���";
					
				case -14:
					return "����û���ҵ���Ӧ�ַ�������";
					
				case -15:
					return "����û���ҵ���Ӧ���㳣��";
					
				case -16:
					return "����֧���������";
					
				case -17:
					return "����û���������";
					
				case -18:
					return "����û�б���";
					
				case -19:
					return "����û������";
					
				case -20:
					return "����û���ұ�����";
					
				case -21:
					return "����������ָ�ⶨ�ķ�Χ";
					
				case -22:
					return "���������ĵ�ַ����ָ���ķ�Χ���������ܳ���EEPROM_LEN-4�����㲻�ܳ���30720-8";
					
				case -23:
					return "���������ĵ�ַ����ָ���ķ�Χ���ַ������ܳ���EEPROM_LEN-LEN������LENΪ�ַ����ĳ���";
					
				case -24:
					return "�����У�����������Ϊ0";
					
				case -25:
					return "δ֪����";
					
				case -26:
					return "�ڶ�����������ָ����λ��";
					
				case -27:
					return "�ַ�����������ָ���ĳ���";
					
				case -28:
					return "�����ַ�������";
					
				case -29:
					return "û�е���������";
					
				case -30:
					return "GOTO�ı�ʶ��䲻��ȫΪ����";
					
				case -31:
					return "���ܴ�ENC�ļ�";
					
				case -32:
					return "���ܶ�ENC�ļ�";
					
				case -33:
					return "GOTO CALL�����ҵ�ָ������תλ��";
					
				case -34:
					return "���ܿ������У�δ֪��������";
					
				case -35:
					return "���ܿ������У�δ֪��������";
					
				case -36:
					return "�ַ������ȳ���50";
					
				case -37:
					return "RIGHT����ʱ����������";
					
				case -38:
					return "��ʶ�ظ�";
					
				case -39:
					return "�����ջ���";
					
				case -40:
					return "��ջ���";
					
				case -41:
					return "���ܽ��������ļ�����鿴�ļ��Ƿ���ֻ�����ԣ��������ļ���";
					
				case -42:
					return "����д�ļ�����鿴�ļ��Ƿ���ֻ�����ԣ��������ļ���";
					
				case -43:
					return "������;ʹ��END������";
					
				case -44:
					return "������ת���ⲿ�Ŀռ�";
					
				case -45:
					return "��������ʧ��";
					
				case -46:
					return "���򳬳������������������ѭ��";
					
				case -47:
					return "д���벻��ȷ";
					
				case -48:
					return "�����벻��ȷ";
					
				case -49:
					return "��дEEPROMʱ����ַ���";
					
				case -50:
					return "USB����ʧ�ܣ�������û���ҵ���ص�ָ��";
					
				case -51:
					return "��USB�ļ����ʧ��";
					
				case -52:
					return "ʹ�ü����������Զ�����ʽ�����ɼ��ܴ���ʱ��������";
					
				case -53:
					return "�޷���usb�豸��������������û�а�װ��û�в����������";
				case -63 :
					return "���ܴ�ָ�����ļ���";
				case -64:
					return  "���ܽ���ָ�����ļ���";
				case -65:
					return  "��֤����󣬿��������������Կ���󣬻�ע����Ȩ�����";
				case -66:
					return   "ִ��TIMEOUT������UPDATE����ʱ�������ID����ID�����";
				case -67:
					return   "ִ��TIMEOUT����ʱ�����ܿ����к����ѵ���";
				case -68:
					return "������������ʱ������Ĳ������ᵼ�·���ֵ��һ������ֵ";
				case -69:
					return  "����û�㹻�ı�������";
				case -70:
					return "���ر����뺯�������";
				case -71:
					return  "������ת���ַ���ʱ��Ҫת��������̫��";
				case  -72: 
					return   "��ʼ������������";
				case   -73: 
					return   "�Ի���������MD5����ʱ����";
				case  -74 :
					return   "MD5��֤IPVAR����";
				case  -75 :
					return   "MD5��֤IPCount����";
				case  -76 :
					return   "û���ҵ���Ӧ��SOCKET����";
				case  -77: 
					return   "û���ҵ�Ҫɾ���Ķ�Ӧ��SOCKET����";
				case  -78 :
					return   "û���ҵ�Ҫɾ���Ķ�Ӧ�Ĺ���ģ�������";
				case -79: 
					return   "û���ҵ�Ҫ���ӵĶ�Ӧ�Ĺ���ģ�������";	
				case  -80: 
					return   "�û����ѳ������Ƶ���Ȩ����";
				case  -81:
					return   "�Ҳ�����Ӧ��INI�ļ���Ŀ";
				case  -82:
					return   "û�н��г�ʼ����������";
				case -252:
					return  "���벻��ȷ";
				case -1088:
					return  "�������ݴ���";
				case -1089:
					return  "��ȡ���ݴ���";
				case -1092:
					return  "�Ҳ�����Ӧ�ķ���˲�����";
				case -1093:
					return  "��ʾ���ӷ���ʱ����";
				case -1095:
					return  "��ȡ��������ʧ��";
				case -1097:
					return  "�������ֽӴ���";
				case -1098:
					return  "�����ֽڶ˿ڴ���";
				case -1099:
					return  "��ʾ��Ч���ӣ����ܽ�����صĲ�����";
				case -2002:
					return  "��ʾ����ʱ��������";
				case -2003:
					return  "��ʾ���͵����ݳ�������յ����ݳ��Ȳ����";
				case -2005:
					return  "��ʾ��ǰ���񲻴����κ�����";
				case -2006:
					return  "��ʾ��ǰ��ѯ�ڵ㳬�����Ϸ���Χ";
				case -2009:
					return  "��ʾ�ر����Ӵ���";
				case -1052:
					return  "�������������Ȩ�Ų���ȷ��";
				case -1053:
					return  "ϵͳ��û���κ���������";	

				default:
					return "δ֪�������";
			}
		}
	}
}
