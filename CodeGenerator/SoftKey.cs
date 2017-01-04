using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CodeGenerator
{
	/// <summary>
	/// Class1 的摘要说明。
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
		public bool b_ini ;//是否已进初始化工作
		public int LastError;//返回最返错误码
		public SoftKey()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//以下函数用于将字节数组转化为宽字符串
		public static string ByteConvertString(byte[] buffer) 
		{
			char []null_string={'\0','\0'};
			System.Text.Encoding encoding = System.Text.Encoding.Default;
			return encoding.GetString(buffer).TrimEnd(null_string);
		}
		//以下函数用于将宽字符串转化为字节数组
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

		//以下函数用于将宽字符串转化为字节数组
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

		//在程序开始运行时，请先调用Ini过程，查找对应的加密锁所在的设备路径；
		//如果找到对应的加密锁，会将该路径保存在变量KeyPath中，
		//以备其它函数的调用；
		 public short Ini() 
		{

			//以下代码是查找对应的加密锁所在的设备路径，该代码由开发工具自动生成，
			 byte [] EncByte={24,0,1,101,73,197,201,221,140,137,143,202,225,210,254,211,110,232,183,194,20,217,75,207,53,121};
			StringBuilder sKeyPath = new StringBuilder("", 260);
			//生成该数组的表达式为：D0=123,D1=123,D2=123,D3=123，由于系统中可能存在多把加密锁，用于查找对应的加密锁
			{
				short ret= FindPort_2(0,EncByte,26,sKeyPath);
				KeyPath = sKeyPath.ToString();
				return ret;
				
			}
			
		}

		//输出函数YCheckKey
//目的：用来对检查是否存在对应的加密锁
//返回结果：为真表示存在对应的加密锁，否则为假
//如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册

 public bool YCheckKey()
{
	int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;
	int pc_d0,pc_d1,pc_d2,pc_d3;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
   //原理：生产随机数，然后在程序端对随机数进行加密运算，然后让加密锁对随数机进行解密运算，看解密后的结果是否与加密前的数据相符，如果相符则为存在对应的加密锁
   //加密强度低，但这个函数必须存在并调用，用于判断是否存在对应加密锁
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return false;}//如果没有进行初始化，进先进行初始化操作
	//以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
	//注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
	//可以在程序中增加对YCheckKeyArray数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
	 byte [] EncByte={57,1,161,189,19,141,88,2,214,215,172,98,13,65,204,160,184,102,242,44,33,174,128,36,165,31,38,204,112,246,36,198,221,144,8,9,229,214,79,195,213,167,59,197,143,204,68,135,8,26,
84,132,106,134,239,84,39,74,239,51,14,187,155,190,49,131,246,250,88,50,198,118,39,192,112,151,120,175,162,98,127,216,191,110,44,186,238,69,228,144,132,57,197,2,219,16,184,102,223,190,
8,153,212,28,87,191,38,204,112,246,36,198,221,144,8,9,229,214,79,195,213,167,106,75,159,47,20,239,160,6,124,164,224,214,230,250,198,191,26,36,66,215,39,88,114,132,185,213,226,8,
242,86,117,249,180,170,72,167,122,32,90,78,215,31,126,4,1,99,75,90,79,111,103,53,254,161,150,53,205,105,75,135,73,80,209,249,110,242,221,240,216,234,158,97,8,9,229,214,79,195,
213,167,59,197,143,204,68,135,8,26,84,132,106,134,239,84,39,74,205,233,64,150,145,144,85,91,155,150,185,41,226,224,39,234,77,167,26,168,141,115,182,226,155,172,10,242,148,187,235,171,
130,125,193,185,193,130,89,55,156,125,121,250,24,255,192,109,249,150,65,246,229,218,29,213,106,75,159,47,20,239,160,6,124,164,224,214,230,250,198,191,186,87,164,130,59,244,96,232,130,125,
193,185,193,130,89,55,156,125,121,250,24,255,192,109,67,61,2,142,2,219,6,172};
	 
	//////////////////////////////////////////////////////////////////////////////////////
	//产生随机数
	System.Random rnd=new System.Random();
	pc_d0=d0 = rnd.Next(0,2147483646)  ;pc_d1=d1 =rnd.Next(0,2147483646); pc_d2=d2 = rnd.Next(0,2147483646);pc_d3=d3 = rnd.Next(0,2147483646);
	//以下在程序端对随机数进行加密运算
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

	//以下将随机数送到加密锁内作解密运算
	
	{
		LastError=CalEx(EncByte, 322,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return false ; }
	//如果相同，则存在对应加密锁，否则，不存在对应的加密锁
	if ((d0 == pc_d0) && (d1 == pc_d1) && (d2 == pc_d2) && (d3 == pc_d3))
	{
		return true;
	}
	return false;
}


//输出函数Ystrcpy
//目的：对字符串进行复制
//参数sr:源字符串
//返回结果：返回结果字符串
//如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册



 public string Ystrcpy(string sr)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
    //原理：先对要参与运算的参数在程序端进行加密运算，然后在锁中对该参数进行解密运算，然后再在锁中将两参数还原，然后返回结果给程序端

   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return "";}//如果没有进行初始化，进先进行初始化操作
	//以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
	//注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
	//可以在程序中增加对YstrcpyArray数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
	 byte [] EncByte={115,1,185,213,226,8,242,86,117,249,180,170,72,167,122,32,90,78,186,87,164,130,59,244,96,232,130,125,193,185,193,130,89,55,156,125,121,250,24,255,192,109,8,117,224,157,202,115,70,124,
190,19,150,176,82,200,134,176,226,181,205,106,153,99,108,72,154,116,92,92,201,139,221,125,237,255,112,195,238,224,228,43,120,30,42,10,175,38,124,231,55,254,33,176,142,25,89,130,130,168,
228,134,50,69,13,226,59,197,143,204,68,135,8,26,84,132,106,134,239,84,39,74,51,39,183,54,109,193,36,143,5,242,240,146,86,246,162,34,222,216,110,85,21,184,241,232,8,117,224,157,
202,115,70,124,190,19,150,176,82,200,134,176,125,86,79,36,149,128,155,142,55,254,33,176,142,25,89,130,130,168,228,134,50,69,13,226,154,116,92,92,201,139,221,125,237,255,112,195,238,224,
228,43,229,201,103,194,157,146,167,33,130,250,245,67,163,67,50,105,94,139,33,69,227,7,200,67,130,168,228,134,50,69,13,226,207,32,48,156,143,83,222,229,220,188,215,100,29,1,233,10,
140,130,141,18,32,214,9,162,210,61,101,81,26,28,99,121,222,216,110,85,21,184,241,232,101,157,224,70,112,147,47,169,175,126,152,93,17,50,243,217,21,26,118,44,158,248,26,245,207,32,
48,156,143,83,222,229,220,188,215,100,29,1,233,10,153,120,61,126,114,104,192,147,200,117,152,217,129,61,135,33,10,186,152,93,251,216,11,231,65,47,170,197,79,161,128,249,52,133,146,99,
154,116,58,120,91,237,96,203,164,17,205,127,99,8,233,125,51,143,201,66,220,214,84,214,188,103,99,150};
	//////////////////////////////////////////////////////////////////////////////////////
	//将要操作的字符串的前12字节分解为3个长整形，赋值给d0-d2,用于加密以后的加密运算
	byte [] temp_string=System.Text.Encoding.Default.GetBytes(sr); int nLen=temp_string.Length;
	byte [] leave_string=new byte[nLen];
	if (nLen>0){StringToDword(ref d0,ref d1,ref d2,temp_string,0);leave_string[0] = 0;}
	//再分解一次，赋值给d3-d5
	if(nLen>12)StringToDword(ref d3,ref d4,ref d5,temp_string,12);
	//获得余下的字符串
    int temp_len = nLen - 24;
    for(n=0;n<temp_len;n++)leave_string[n] = temp_string[24 + n];
	//以下在程序端对输入参数进行加密运算
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

	//以下将加密后的参数送到加密锁内作解密运算，并将解密后的结果合并为字符串，
	
	{
		LastError=CalEx(EncByte, 378,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return "" ; }
	//如果没有返回错误，则将解密并运算的结果返回;
	
	//创建临时目的缓冲区
	StringBuilder Temp_byte=new StringBuilder("",nLen);
	//然后将结果字符串复制到目的的字符串中
	if(nLen>0)
	{
		{
			lstrcpy (Temp_byte, s0.ToString());
			//再将分解剩余的的字符串连接到目的字符串中
			lstrcat (Temp_byte, leave_string);
		}
	}
	return Temp_byte.ToString();
}


//输出函数YCompareString
//目的：用来对两个字符串进行比较
//参数ins1：要比较的两个字符串之一
//参数ins2：要比较的两个字符串之一
//返回结果：如果为真，表明两个字符串相等，否则为假
//如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册



 public bool YCompareString(string ins1,string ins2)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
    //原理：先对要参与运算的参数在程序端进行加密运算，然后在锁中对该参数进行解密运算并进行比较，然后返回结果给程序端
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return false;}//如果没有进行初始化，进先进行初始化操作
	//以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
	//注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
	//可以在程序中增加对YCompareStringArray数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
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
	//将第一个字符串的前12字节分解为3个长整形，赋值给d0-d2,用于加密以后的加密运算
	byte [] temp_string_1=System.Text.Encoding.Default.GetBytes(ins1); int nLen_1=temp_string_1.Length;
	byte [] leave_string_1=new byte[nLen_1];
	if(nLen_1>0){StringToDword(ref d0,ref d1,ref d2,temp_string_1,0);leave_string_1[0] = 0;}
	//获得余下的字符串
    int temp_len = nLen_1 - 12;
    for(n=0;n<temp_len;n++)leave_string_1[n] = temp_string_1[12 + n];
	//将第二个字符串的前12字节分解为3个长整形，赋值给d3-d5,用于加密以后的加密运算
	byte [] temp_string_2=System.Text.Encoding.Default.GetBytes(ins2); int nLen_2=temp_string_2.Length;
	byte [] leave_string_2=new byte[nLen_2];
	if(nLen_2>0){StringToDword(ref d3,ref d4,ref d5,temp_string_2,0);leave_string_2[0] = 0;}
	//获得余下的字符串
	temp_len = nLen_2 - 12;
	for(n=0;n<temp_len;n++)leave_string_2[n] = temp_string_2[12 + n];
	//以下在程序端对输入参数进行加密运算
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

	//以下将加密后的参数送到加密锁内作解密运算，并将解密后的结果合并为字符串，再进行比较
	
	{
		LastError=CalEx(EncByte, 410,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return false ; }
	//如果没有返回错误，则将解密并运算的结果返回;
	if(d7==0)return false;//如果d7不为0表明在锁内进行比较的结果不相符，直接返回假
	//如果锁内比较相符，再比较余下的部分
	return (lstrcmp(temp_string_1,temp_string_2)==0);;
}

//输出函数Ystrcat
//目的：用来对两个字符串进行连接
//参数ins1：要连接的两个字符串之一
//参数ins2：要连接的两个字符串之一
//返回结果：返回连接后的字符串
//如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册



 public string Ystrcat(string ins1,string ins2)
{
   int d0=0,d1=0,d2=0,d3=0,d4=0,d5=0,d6=0,d7=0;int n;
	float [] F=new float[8];
	StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
    StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
	
   //原理：先对要参与运算的参数在程序端进行加密运算，然后在锁中对该参数进行解密运算，然后再在锁中将两参数还原，然后返回结果给程序端
   LastError=0;
   if(!b_ini){LastError=Ini();if(LastError!=0)return "";}//如果没有进行初始化，进先进行初始化操作
	//以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
	//注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
	//可以在程序中增加对YstrcatArray数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
	 byte [] EncByte={115,1,20,191,100,39,113,171,112,205,37,108,170,153,26,138,244,204,80,80,40,181,50,91,198,5,217,137,121,81,45,63,18,36,112,151,120,175,162,98,127,216,185,213,226,8,242,86,117,249,
180,170,72,167,122,32,90,78,111,153,200,205,58,97,6,23,192,215,9,206,254,200,81,75,21,26,118,44,158,248,26,245,155,150,185,41,226,224,39,234,77,167,26,168,141,115,182,226,32,111,
171,32,100,9,196,47,165,78,219,189,24,16,160,109,156,125,121,250,24,255,192,109,191,110,44,186,238,69,228,144,132,57,197,2,219,16,184,102,119,212,115,124,255,36,27,156,94,139,33,69,
227,7,200,67,130,168,228,134,50,69,13,226,8,117,224,157,202,115,70,124,190,19,150,176,82,200,134,176,222,176,215,180,25,46,105,172,5,242,240,146,86,246,162,34,222,216,110,85,21,184,
241,232,8,117,224,157,202,115,70,124,190,19,150,176,82,200,134,176,170,242,38,212,202,46,245,166,217,137,121,81,45,63,18,36,112,151,120,175,162,98,127,216,213,83,123,129,112,13,178,124,
120,230,184,217,241,108,76,11,67,172,186,58,158,175,195,144,215,172,254,65,183,160,8,183,192,215,9,206,254,200,81,75,21,26,118,44,158,248,26,245,173,75,221,28,109,14,14,134,154,116,
92,92,201,139,221,125,237,255,112,195,238,224,228,43,2,45,217,138,64,3,107,126,103,238,230,227,178,171,210,156,243,108,221,12,60,179,74,137,54,84,73,55,45,150,64,255,41,2,89,137,
116,111,158,70,208,229,125,12,155,230,38,10,148,5,71,98,41,182,140,197,105,233,231,228,126,220,1,246};

	//////////////////////////////////////////////////////////////////////////////////////
	//将第一个字符串的前12字节分解为3个长整形，赋值给d0-d2,用于加密以后的加密运算
	byte [] temp_string_1=System.Text.Encoding.Default.GetBytes(ins1); int nLen_1=temp_string_1.Length;
	byte [] leave_string_1=new byte[nLen_1];
	if(nLen_1>0){StringToDword(ref d0,ref d1,ref d2,temp_string_1,0);leave_string_1[0] = 0;}
	//获得余下的字符串
    int temp_len = nLen_1 - 12;
    for(n=0;n<temp_len;n++)leave_string_1[n] = temp_string_1[12 + n];
	//将第二个字符串的前12字节分解为3个长整形，赋值给d3-d5,用于加密以后的加密运算
	byte [] temp_string_2=System.Text.Encoding.Default.GetBytes(ins2); int nLen_2=temp_string_2.Length;
	byte [] leave_string_2=new byte[nLen_2];
	if(nLen_2>0){StringToDword(ref d3,ref d4,ref d5,temp_string_2,0);leave_string_2[0] = 0;}
	//获得余下的字符串
	temp_len = nLen_2 - 12;
	for(n=0;n<temp_len;n++)leave_string_2[n] = temp_string_2[12 + n];
	//以下在程序端对输入参数进行加密运算
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

	//以下将加密后的参数送到加密锁内作解密运算，并将解密后的结果合并为字符串，
	
	{
		LastError=CalEx(EncByte, 378,ref d0,ref d1,ref d2,ref d3,ref d4,ref d5,ref d6,ref d7,
						ref F[0],ref F[1],ref F[2],ref F[3],ref F[4],ref F[5],ref F[6],ref F[7],
						s0,s1,s2,s3,s4,s5,s6,s7,KeyPath,20000000); 
	}
	if (LastError!= 0 && LastError!= -43) {return "" ; }
	//如果没有返回错误，则将解密并运算的结果返回;
	
	//创建临时目的缓冲区
	StringBuilder Temp_byte=new StringBuilder("",nLen_1+nLen_2);
	//然后将结果字符串复制到目的的字符串中
	if(nLen_1+nLen_2>0)
	{
		{
			lstrcpy (Temp_byte, s0.ToString());
			//再将分解剩余的的字符串连接到目的字符串中
			lstrcat (Temp_byte, leave_string_1);
			//再连接第二个字符串的加密并解密后的部分
			lstrcat_2 (Temp_byte, s1.ToString());
			//再连接余下的部分
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
					return "未找到返回结果变量";

				case -2:
					return "未找到 = 符号";
					
				case -3:
					return "代表没有找到相应常数";
					
				case -5:
					return "代表找不到字符串的第一个双引号";
					
				case -6:
					return "代表找不到字符串的第二个双引号";
					
				case -7:
					return "IF语句没有找到goto字符";
					
				case -8:
					return "IF语句没有找到第一个比较字符";
					
				case -9:
					return "IF语句没有找到比较符号";
					
				case -10:
					return "两边变量类型不相符";
					
				case -11:
					return "没有找到NOT符号";
					
				case -12:
					return "不是整形变量";
					
				case -13:
					return "代表没有找到相应整形常数";
					
				case -14:
					return "代表没有找到相应字符串常数";
					
				case -15:
					return "代表没有找到相应浮点常数";
					
				case -16:
					return "代表不支持这个运算";
					
				case -17:
					return "代表没有左边括号";
					
				case -18:
					return "代表没有变量";
					
				case -19:
					return "代表没“，”";
					
				case -20:
					return "代表没有右边括号";
					
				case -21:
					return "代表常数超过指这定的范围";
					
				case -22:
					return "代表储存器的地址超过指定的范围，整数不能超过EEPROM_LEN-4，浮点不能超过30720-8";
					
				case -23:
					return "代表储存器的地址超过指定的范围，字符串不能超过EEPROM_LEN-LEN，其中LEN为字符串的长度";
					
				case -24:
					return "除法中，被除数不能为0";
					
				case -25:
					return "未知错误";
					
				case -26:
					return "第二个变量不在指定的位置";
					
				case -27:
					return "字符串常量超过指定的长度";
					
				case -28:
					return "不是字符串变量";
					
				case -29:
					return "没有第三个变量";
					
				case -30:
					return "GOTO的标识语句不能全为数字";
					
				case -31:
					return "不能打开ENC文件";
					
				case -32:
					return "不能读ENC文件";
					
				case -33:
					return "GOTO CALL不能找到指定的跳转位置";
					
				case -34:
					return "智能卡运算中，未知数据类型";
					
				case -35:
					return "智能卡运算中，未知代码类型";
					
				case -36:
					return "字符串长度超出50";
					
				case -37:
					return "RIGHT操作时超长，负长";
					
				case -38:
					return "标识重复";
					
				case -39:
					return "程序堆栈溢出";
					
				case -40:
					return "堆栈溢出";
					
				case -41:
					return "不能建立编译文件，请查看文件是否有只读属性，或被其它文件打开";
					
				case -42:
					return "不能写文件，请查看文件是否有只读属性，或被其它文件打开";
					
				case -43:
					return "程序被中途使用END语句结束";
					
				case -44:
					return "程序跳转到外部的空间";
					
				case -45:
					return "传送数据失败";
					
				case -46:
					return "程序超出运算次数，可能是死循环";
					
				case -47:
					return "写密码不正确";
					
				case -48:
					return "读密码不正确";
					
				case -49:
					return "读写EEPROM时，地址溢出";
					
				case -50:
					return "USB操作失败，可能是没有找到相关的指令";
					
				case -51:
					return "打开USB文件句柄失败";
					
				case -52:
					return "使用加密锁加密自定义表达式，生成加密代码时生产错误";
					
				case -53:
					return "无法打开usb设备，可能驱动程序没有安装或没有插入加密锁。";
				case -63 :
					return "不能打开指定的文件。";
				case -64:
					return  "不能建立指定的文件。";
				case -65:
					return  "验证码错误，可能是输入解密密钥错误，或注册授权码错误";
				case -66:
					return   "执行TIMEOUT函数或UPDATE函数时，输入的ID与锁ID不相符";
				case -67:
					return   "执行TIMEOUT函数时，智能卡运行函数已到期";
				case -68:
					return "操作浮点运算时，输入的参数将会导致返回值是一个无穷值";
				case -69:
					return  "代表没足够的变量参数";
				case -70:
					return "返回变量与函数不相符";
				case -71:
					return  "浮点数转换字符串时，要转换的数据太大。";
				case  -72: 
					return   "初始化服务器错误";
				case   -73: 
					return   "对缓冲区进行MD5运算时错误";
				case  -74 :
					return   "MD5验证IPVAR错误";
				case  -75 :
					return   "MD5验证IPCount错误";
				case  -76 :
					return   "没有找到对应的SOCKET连接";
				case  -77: 
					return   "没有找到要删除的对应的SOCKET连接";
				case  -78 :
					return   "没有找到要删除的对应的功能模块号连接";
				case -79: 
					return   "没有找到要增加的对应的功能模块号连接";	
				case  -80: 
					return   "用户数已超过限制的授权数量";
				case  -81:
					return   "找不到对应的INI文件条目";
				case  -82:
					return   "没有进行初始化服务工作。";
				case -252:
					return  "密码不正确";
				case -1088:
					return  "发送数据错误";
				case -1089:
					return  "获取数据错误";
				case -1092:
					return  "找不到对应的服务端操作码";
				case -1093:
					return  "表示连接服务时错误";
				case -1095:
					return  "获取主机名称失败";
				case -1097:
					return  "建立套字接错误";
				case -1098:
					return  "绑定套字节端口错误";
				case -1099:
					return  "表示无效连接，不能进行相关的操作。";
				case -2002:
					return  "表示监听时产生错误";
				case -2003:
					return  "表示发送的数据长度与接收的数据长度不相符";
				case -2005:
					return  "表示当前服务不存在任何连接";
				case -2006:
					return  "表示当前查询节点超出集合范范围";
				case -2009:
					return  "表示关闭连接错误";
				case -1052:
					return  "可能是输入的授权号不正确。";
				case -1053:
					return  "系统上没有任何智能锁。";	

				default:
					return "未知错误代码";
			}
		}
	}
}
