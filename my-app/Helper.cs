using System.Collections.Generic;
using System.Drawing;
using System;
using static System.Math;
using static Structs;

public static class Helper
{
    public static Color[] Chunk(this Color[] col, int pos, int n)
    {
      // n  : total number of chunks in a line
      // pos: this chunk's number
      int size = (int)Round(Sqrt(col.Length));
      int rs = size/n;
      Color[] newCol = new Color[rs*rs];
      for (int i=0; i<rs; i++)
      {
        for (int j=0; j<rs; j++)
        {
          int x = pos%n, y = pos/n;
          int _i = x*rs+i, _j = y*rs+j;
          newCol[i+rs*j] = col[_i+size*_j];
        }
      }
      return newCol;
    }
    public static Color[] Scale2x(this Color[] old)
    {
      int n = (int)Round(Sqrt(old.Length));
      int size = 2*n;
      Color[] co = new Color[size*size];

      for (int i=0; i<n; i++)
      {
        for (int j=0; j<n; j++)
        {
          int bin = 0;
          Vector2Int p = new Vector2Int(i,j);
          Vector2Int[] nb = p.Neighbours(n);
            //   +---+---+---+
            //   | 7 | 2 | 4 |
            //   +---+---+---+
            //   | 1 | p | 0 |
            //   +---+---+---+
            //   | 5 | 3 | 6 |
            //   +----+---+---+
          for (int k=0; k<nb.Length; k++)
          {
            if (old[nb[k].x+n*nb[k].y]!=old[i+n*j])
            {
              bin+= (int)Round(Pow(2,k));
                //    0   1   2   3   4   5   6   7
                //    1   2   4   8  16  32  64 128
            }
          }
          Color[] newcol = new Color[4]; // [(0,0),(1,0),(0,1),(1,1)]
                                         //   +---+---+
                                         //   | 2 | 3 |
                                         //   +---+---+
                                         //   | 0 | 1 |
                                         //   +---+---+
          for (int k=0; k<4; k++)
          {
            newcol[k] = old[p.x+n*p.y];
          }
          switch(bin)
          {
            default: // case 0 // [0,1,2](37) + [3](52) +[4](50) + [5](20) + [6](2)
              // do nothing
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 142: // [4](1) - *123***7
              newcol[2] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 23:  // [4](1) - 012*4***
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   |   | 2 |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 77:  // [4](1) - 0*23**6*
              newcol[1] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              break;
            case 43:  // [4](1) - 01*3*5**
              newcol[0] = old[nb[3].x+n*nb[3].y];
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   | 3 |   |
              //   +---+---+
              break;
            case 134: // [3](1) - *12****7
            case 135: // [4](2) - 012****7
            case 198: //        - *12***67
            case 199: // [5](3) - 012***67
            case 206: //        - *123**67
            case 143: //        - 0123***7
              newcol[2] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 |   |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 21:  // [3](1) - 0*2*4***
            case 29:  // [4](2) - 0*234***
            case 53:  //        - 0*2*45**
            case 55:  // [5](3) - 012*45**
            case 61:  //        - 0*2345**
            case 31:  //        - 01234***
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 74:  // [3](1) - 0**3**6*
            case 76:  // [4](2) - 01*3**6*
            case 201: //        - 0**3**67
            case 205: // [5](3) - 0*23**67
            case 203: //        - 01*3**67
            case 79:  //        - 0123**6*
              newcol[1] = old[nb[3].x+n*nb[3].y];
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   |   | 3 |
              //   +---+---+
              break;
            case 42:  // [3](1) - *1*3*5**
            case 46:  // [4](2) - *123*5**
            case 58:  //        - *1*345**
            case 62:  // [5](3) - *12345**
            case 59:  //        - 01*345**
            case 47:  //        - 0123*5**
              newcol[0] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              break;
            case 149: // [4](2) - 0*2*4**7
            case 150: //        - *12*4**7
            case 157: // [5](5) - 0*234**7
            case 158: //        - *1234**7
            case 181: //        - 0*2*45*7
            case 214: //        - *12*4*67
            case 151: //        - 012*4**7
            case 189: // [6](2) - 0*2345*7
            case 222: //        - *1234*67
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 | 2 |
              //   +---+---+
              //   |   |   |
              //   +---+---+
              break;
            case 105: // [4](2) - 0**3*56*
            case 106: //        - *1*3*56*
            case 109: // [5](5) - 0*23*56*
            case 110: //        - *123*56*
            case 122: //        - *1*3456*
            case 233: //        - 0**3*567
            case 107: //        - 01*3*56*
            case 126: // [6](2) - *123456*
            case 237: //        - 0*23*567
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              //   +---+---+
              //   |   |   |
              //   +---+---+
              //   | 3 | 3 |
              //   +---+---+
              break;
            case 97:  // [4](2) - 0**34*6*
            case 85:  //        - 0*2*4*6*
            case 87:  // [5](5) - 012*4*6*
            case 91:  //        - 01*34*6*
            case 217: //        - 0**34*67
            case 117: //        - 0*2*456*
            case 93:  //        - 0*234*6*
            case 119: // [6](2) - 012*456*
            case 231: //        - 012**567
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              break;
            case 170: // [4](2) - *1*3*5*7
            case 166: //        - *12**5*7
            case 171: // [5](5) - 01*3*5*7
            case 167: //        - 012**5*7
            case 230: //        - *12**567
            case 186: //        - *1*345*7
            case 174: //        - *123*5*7
            case 219: // [6](2) - 01*34*67
            case 187: //        - 01*345*7
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              break;
            case 207: // [6](1) - 0123**67
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              break;
            case 63:  // [6](1) - 012345**
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   |   | 2 |
              //   +---+---+
              //   | 3 |   |
              //   +---+---+
              break;
            case 121: // [5](1) - 0**3456*
            case 125: // [6](1) - 0*23456*
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              //   | 3 | 0 |
              //   +---+---+
              break;
            case 234: // [5](1) - *1*3*567
            case 235: // [6](1) - 01*3*567
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              //   | 3 | 3 |
              //   +---+---+
              break;
            case 182: // [5](1) - *12*45*7
            case 190: // [6](1) - *12345*7
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 1 | 2 |
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              break;
            case 213: // [5](1) - 0*2*4*67
            case 215: // [6](1) - 012*4*67
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 | 2 |
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              break;
            case 123: // [6](2) - 01*3456*
            case 249: //        - 0**34567
            case 127: // [7](1) - 0123456*
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              //   | 3 | 3 |
              //   +---+---+
              break;
            case 238: // [6](2) - *123*567
            case 250: //        - *1*34567
            case 239: // [7](1) - 0123*567
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              //   | 1 | 3 |
              //   +---+---+
              break;
            case 221: // [6](2) - 0*234*67
            case 245: //        - 0*2*4567
            case 223: // [7](1) - 01234*67
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   | 2 | 0 |
              //   +---+---+
              //   |   | 0 |
              //   +---+---+
              break;
            case 183: // [6](2) - 012*45*7
            case 246: //        - *12*4567
            case 191: // [7](1) - 012345*7
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 | 2 |
              //   +---+---+
              //   | 1 |   |
              //   +---+---+
              break;
            case 251: // [7](1) - 01*34567
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   | 1 | 0 |
              //   +---+---+
              //   | 3 | 3 |
              //   +---+---+
              break;
            case 254: // [7](1) - *1234567
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 1 | 2 |
              //   +---+---+
              //   | 1 | 3 |
              //   +---+---+
              break;
            case 247: // [7](1) - 012*4567
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 | 2 |
              //   +---+---+
              //   | 1 | 0 |
              //   +---+---+
              break;
            case 253: // [7](1) - 0*234567
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   | 2 | 0 |
              //   +---+---+
              //   | 3 | 0 |
              //   +---+---+
              break;
            case 159: // [6](2) - 01234**7
            case 111: //        - 0123*56*
              newcol[0] = old[nb[3].x+n*nb[3].y];
              newcol[1] = old[nb[3].x+n*nb[3].y];
              newcol[2] = old[nb[2].x+n*nb[2].y];
              newcol[3] = old[nb[2].x+n*nb[2].y];
              //   +---+---+
              //   | 2 | 2 |
              //   +---+---+
              //   | 3 | 3 |
              //   +---+---+
              break;
            case 95:  // [6](2) - 01234*6*
            case 175: //        - 0123*5*7
            case 255: // [8](1) - 01234567
              newcol[0] = old[nb[1].x+n*nb[1].y];
              newcol[1] = old[nb[0].x+n*nb[0].y];
              newcol[2] = old[nb[1].x+n*nb[1].y];
              newcol[3] = old[nb[0].x+n*nb[0].y];
              //   +---+---+
              //   | 1 | 0 |
              //   +---+---+
              //   | 1 | 0 |
              //   +---+---+
              break;
          }
          co[2*i+size*(2*j)] = newcol[0];
          co[2*i+1+size*(2*j)] = newcol[1];
          co[2*i+size*(2*j+1)] = newcol[2];
          co[2*i+1+size*(2*j+1)] = newcol[3];
        }
      }

      return co;
    }
    public static Bitmap Chunk(this Bitmap bmp, int pos, int n)
    {
      // n  : total number of chunks in a line
      // pos: this chunk's number
      int rs = bmp.Width/n;
      Bitmap newBmp = new(rs,rs);
      for (int i=0; i<rs; i++)
      {
        for (int j=0; j<rs; j++)
        {
          int x = pos%n;
          int y = pos/n;
          int _i = x*rs+i;
          int _j = y*rs+j;
          newBmp.SetPixel(i,j,bmp.GetPixel(_i,_j));
        }
      }
      return newBmp;
    }
    public static Bitmap Scale2x(this Bitmap old)
    {
      int size = old.Width;
      Bitmap bmp = new(2*size,2*size);
      Color[] col = new Color[size*size];
      for (int i=0; i<size; i++){
        for (int j=0; j<size; j++){
          col[j*size + i] = old.GetPixel(i,j);
        }
      }
      
      col = col.Scale2x();
      for (int i=0; i<2*size; i++){
        for (int j=0; j<2*size; j++){
          bmp.SetPixel(i,j,col[j*2*size+i]);
        }
      }

      return bmp;
    }
    public static Vector2Int[] Neighbours(this Vector2Int point, int size)
    {
      Vector2Int[] neighbours = new Vector2Int[8]
      {
        new Vector2Int(Min(point.x+1,size-1),point.y),                      // + 0
        new Vector2Int(Max(point.x-1,0),point.y),                           // - 0
        new Vector2Int(point.x,Min(point.y+1,size-1)),                      // 0 +
        new Vector2Int(point.x,Max(point.y-1,0)),                           // 0 -
        new Vector2Int(Min(point.x+1,size-1),Min(point.y+1,size-1)),  // + +
        new Vector2Int(Max(point.x-1,0),Max(point.y-1,0)),            // - -
        new Vector2Int(Min(point.x+1,size-1),Max(point.y-1,0)),       // + -
        new Vector2Int(Max(point.x-1,0),Min(point.y+1,size-1))        // - +
      };
      return neighbours;
    }
    public static T[] Shuffle<T>(this T[] array, Random rnd)
    {
      int n = array.Length;
      while (n>1)
      {
        int k = rnd.Next(n--);
        T temp = array[n];
        array[n] = array[k];
        array[k] = temp;
      }
      return array;
    }
    public static List<T> Shuffle<T>(this List<T> list, Random rnd)
    {
      int n = list.Count;
      while (n>1)
      {
        int k = rnd.Next(n--);
        T temp = list[n];
        list[n] = list[k];
        list[k] = temp;
      }
      return list;
    }
}