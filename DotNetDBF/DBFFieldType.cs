/*
 DBFFieldType
 Class for reading the records assuming that the given
 InputStream comtains DBF data.
 
 This file is part of DotNetDBF packege.
 
 author (DotNetDBF): Jay Tuley <jay+dotnetdbf@tuley.name> 6/28/2007
 
 License: LGPL (http://www.gnu.org/copyleft/lesser.html)
 
 */

using System.Data;

namespace DotNetDBF
{
    public enum NativeDbType :byte
    {
        //Autoincrement = (byte) 0x2B, //+ in ASCII
        //Timestamp = (byte) 0x40, //@ in ASCII
        //Binary = (byte) 0x42, //B in ASCII
        //Char = (byte) 0x43, //C in ASCII
        //Date = (byte) 0x44, //D in ASCII
        //Float = (byte) 0x46, //F in ASCII
        //Ole = (byte) 0x47, //G in ASCII
        //Long = (byte) 0x49, //I in ASCII
        //Logical = (byte) 0x4C, //L in ASCII
        //Memo = (byte) 0x4D, //M in ASCII
        //Numeric = (byte) 0x4E, //N in ASCII
        //Double = (byte) 0x4F, //O in ASCII

        //VFP
        //http://msdn.microsoft.com/en-us/library/aa975386(v=vs.71).aspx
        //Field type: 
        //C   ¡V   Character
        //Y   ¡V   Currency
        //N   ¡V   Numeric
        //F   ¡V   Float
        //D   ¡V   Date
        //T   ¡V   DateTime
        //B   ¡V   Double
        //I   ¡V   Integer
        //L   ¡V   Logical
        //M   ¡V Memo
        //G   ¡V General
        //C   ¡V   Character (binary)
        //M   ¡V   Memo (binary)
        //P   ¡V   Picture

        Char = (byte)'C',
        Currency = (byte)'Y',
        Numeric = (byte)'N',
        Float = (byte)'F',
        Date = (byte)'D',
        DateTime = (byte)'T',
        Double = (byte)'B',
        Integer = (byte)'I',
        Logical = (byte)'L',
        Memo = (byte)'M',
        General = (byte)'G',
        Picture = (byte)'P',
    }

    static public class DBFFieldType
    {
        public const byte EndOfData = 0x1A; //^Z End of File
        public const byte EndOfField = 0x0D; //End of Field
        public const byte False = 0x46; //F in Ascci
        public const byte Space = 0x20; //Space in ascii
        public const byte True = 0x54; //T in ascii
        public const byte UnknownByte = 0x3F; //Unknown Bool value
        public const string Unknown = "?"; //Unknown value
        static public DbType FromNative(NativeDbType aByte)
        {
            switch (aByte)
            {
                case NativeDbType.Char:
                    return DbType.AnsiStringFixedLength;
                case NativeDbType.Logical:
                    return DbType.Boolean;
                case NativeDbType.Currency:
                    return DbType.Currency;
                case NativeDbType.Numeric:
                case NativeDbType.Integer:
                    return DbType.Decimal;
                case NativeDbType.Date:
                    return DbType.Date;
                case NativeDbType.DateTime:
                    return DbType.DateTime;
                case NativeDbType.Float:
                case NativeDbType.Double:
                    return DbType.Double;
                case NativeDbType.Memo:
                    return DbType.AnsiString;
                default:
                    throw new DBFException(
                        string.Format("Unsupported Native Type {0}", aByte));
            }
        }

        static public NativeDbType FromDbType(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiStringFixedLength:
                    return NativeDbType.Char;
                case DbType.Boolean:
                    return NativeDbType.Logical;
                case DbType.Decimal:
                    return NativeDbType.Numeric;
                case DbType.Date:
                    return NativeDbType.Date;
                case DbType.DateTime:
                    return NativeDbType.DateTime;
                case DbType.Double:
                    return NativeDbType.Double;
                case DbType.AnsiString:
                    return NativeDbType.Memo;
                case DbType.Currency:
                    return NativeDbType.Currency;
                default:
                    throw new DBFException(
                        string.Format("Unsupported Type {0}", dbType));
            }
        }
    }
}