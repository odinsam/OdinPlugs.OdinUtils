### **string 安全操作封装**

#### 1. RSA 加密、解密

RSA 加密 - 支持分段加密

```csharp
// 加密原文
string str = "odinsam";
// 私钥签名
string sign = RsaHelper.Sign(str, privateKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1, EnumRsaKeyType.PKCS8);
// 公钥验签
bool flag = RsaHelper.Verify(str, sign, publicKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
// 公钥加密
string encryptStr = RsaHelper.RsaEncrypt(str, publicKey);
// 私钥解密
string decryptStr = RsaHelper.RsaDecrypt(enStr, privateKey, EnumRsaKeyType.PKCS8)
```

RsaHelper 中包含各种秘钥格式的转换，包括

私钥 Pem 格式转换为 xml 格式

私钥格式转换 xml->pkcs1
私钥格式转换 xml->pkcs8

私钥格式转换 pkcs1 -> xml
私钥格式转换 pkcs8 -> xml

私钥格式转换 pkcs1-> pkcs8
私钥格式转换 pkcs8-> pkcs1

---

公钥格式转换
xml->pem、公钥格式转换 pem->xml

#### 2. AES、MD5 加密、解密

```csharp
// aes 加密字符串
var encryptStr = StringAesDes.EncryptByAES("原文",密钥（32位,可选参数),加密偏移量);
// aes 解密字符串
var decryptStr = StringAesDes.DecryptByAES("密文",密钥（32位,可选参数),加密偏移量);
// md5 加密
var str = "value".ToMd5Lower("加密盐值，可选参数");
var str = "value".ToMd5Upper("加密盐值，可选参数");
// md5 两次加密  加密规则:    (str+salt).md5().md5();
var str = "value".ToMd5Lower2("加密盐值，可选参数");
var str = "value".ToMd5Upper2("加密盐值，可选参数");
```

#### 3. string、stream sha256 加密

```csharp
// string sha256 加密
var str = "string value";
string encryptStr = str.Sha256();
// stream sha256 加密
Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
string encryptStr = stream.Sha256();
```

#### 4. 转码、掩码

```csharp
// 将指定的16进制字符串转换为byte数组
byte[] bts = StringEncodeConvert.HexStringToByteArray(string s);

// 将一个byte数组转换成一个格式化的16进制字符串
string str = StringEncodeConvert.ByteArrayToHexString(byte[] data);

// 微信掩码
StringSecurity.AddWxNameMark(this Object entity, string fieldName, string newfieldName);

/// <summary>
/// 微信掩码
/// <code>
/// 掩码规则： AA******  保留前两位，后续全部使用 * 掩码
/// </code>
/// </summary>
/// <param name="entity">用户对象</param>
/// <param name="fieldName">需要掩码的字段</param>
/// <param name="newfieldName">掩码后的新字段</param>
/// <returns>具有掩码字段的对象</returns>
JObject obj = StringSecurity.AddWxNameMark(this Object entity, string fieldName, string newfieldName);

/// <summary>
/// 邮箱掩码
/// <code>
/// 掩码规则：如果 @前的邮箱名 length>3 那么 A***B 收尾保留其余用 * 代替， 否则 不做掩码处理
/// </code>
/// </summary>
/// <param name="entity">用户对象</param>
/// <param name="fieldName">需要掩码的字段</param>
/// <param name="newfieldName">掩码后的新字段</param>
/// <returns>具有掩码字段的对象</returns>
JObject obj = StringSecurity.AddMailMark(this Object entity, string fieldName, string newfieldName);

/// <summary>
/// 姓名掩码
/// <code>
/// 掩码规则：1. length=2 A*  2. length=3  A*b   3. 其余 A**B，收尾保留其余用 * 代替
/// </code>
/// </summary>
/// <param name="entity">用户对象</param>
/// <param name="fieldName">需要掩码的字段</param>
/// <param name="newfieldName">掩码后的新字段</param>
/// <returns>具有掩码字段的对象</returns>
JObject obj = StringSecurity.AddNameMark(this Object entity, string fieldName, string newfieldName);

/// <summary>
/// 身份证掩码
/// <code>
/// 掩码规则：  ABCDEFGHIJ*******K, 获取前10位和最后一位，奇遇用 掩码 代码
/// </code>
/// </summary>
/// <param name="entity">用户对象</param>
/// <param name="fieldName">需要掩码的字段</param>
/// <param name="newfieldName">掩码后的新字段</param>
/// <param name="strMark">掩码规则，可选参数。默认规则:  $1*******$2 </param>
/// <returns>具有掩码字段的对象</returns>
JObject obj = StringSecurity.AddCardIdMark(this Object entity, string fieldName, string newfieldName = null, string strMark = null);

/// <summary>
/// 电话号码掩码
/// <code>
/// 掩码规则：  ABC****DEFGHIJ,  前3位 和 后7位，其余用 掩码 代替
/// </code>
/// </summary>
/// <param name="entity">用户对象</param>
/// <param name="fieldName">需要掩码的字段</param>
/// <param name="newfieldName">掩码后的新字段</param>
/// <param name="strMark">掩码规则，可选参数。默认规则: $1****$2 </param>
/// <returns>具有掩码字段的对象</returns>
JObject obj = StringSecurity.AddPhoneMark(this Object entity, string fieldName, string newfieldName = null, string strMark = null);

/// <summary>
/// dm5 加密 转小写
/// </summary>
/// <param name="str">需要加密的字符串</param>
/// <param name="length"></param>
/// <returns></returns>
string str = StringSecurity.ToMd5Lower(this string str, int length = 32);

/// <summary>
/// dm5 加密 转大写
/// </summary>
/// <param name="str">需要加密的字符串</param>
/// <param name="length"></param>
/// <returns></returns>
string str = StringSecurity.ToMd5Upper(this string str, int length = 32);
```

### 5. 字符串正则

```csharp
/// 是否是中国大陆身份证号码    默认正则: (\\d{10})\\d{7}([\\dxX]{1})
bool flag = "str".IsChinaCardId("身份证号正则表达式，可选");

/// 判断中国移动电话号码格式    默认正则: (\\d{3})\\d{4}(\\d{4})
bool flag = "str".IsChinaMobile("身份证号正则表达式，可选");

/// 判断邮箱地址格式    默认正则: ^([a-zA-Z0-9_-])+.*([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$
bool flag = "str".IsEmail("身份证号正则表达式，可选");

/// 判断uri格式    默认正则: (https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]
bool flag = "str".IsUri("身份证号正则表达式，可选");
```

### 6. 万年历、农历、属性等日期操作

```csharp
DateTime dt = DateTime.Now;
ChineseCalendar cc = new ChineseCalendar(dt);
Console.WriteLine("阳历：" + cc.DateString);
Console.WriteLine("属相：" + cc.AnimalString);
Console.WriteLine("农历：" + cc.ChineseDateString);
Console.WriteLine("时辰：" + cc.ChineseHour);
Console.WriteLine("节气：" + cc.ChineseTwentyFourDay);
Console.WriteLine("节日：" + cc.DateHoliday);
Console.WriteLine("前一个节气：" + cc.ChineseTwentyFourPrevDay);
Console.WriteLine("后一个节气：" + cc.ChineseTwentyFourNextDay);
Console.WriteLine("干支：" + cc.GanZhiDateString);
Console.WriteLine("星期：" + cc.WeekDayStr);
Console.WriteLine("星宿：" + cc.ChineseConstellation);
Console.WriteLine("星座：" + cc.Constellation);
```
