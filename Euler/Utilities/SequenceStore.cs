using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Euler.Utilities
{
  public class SequenceStore : IList<long>
  {
    private readonly string mName;
    private readonly IList<long> mList;

    public SequenceStore(string name, IEnumerable<long> defaultList)
    {
      mName = name;
      mList = LoadFromStore(defaultList);
    }

    #region Implementation of IEnumerable

    public IEnumerator<long> GetEnumerator()
    {
      return mList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion

    #region Implementation of ICollection<long>

    public void Add(long item)
    {
      mList.Add(item);

      if (mList.Count % 10000 == 0)
      {
        SaveToStore(mList);
      }
    }

    public void Clear()
    {
      mList.Clear();
    }

    public bool Contains(long item)
    {
      return mList.Contains(item);
    }

    public void CopyTo(long[] array, int arrayIndex)
    {
      mList.CopyTo(array, arrayIndex);
    }

    public bool Remove(long item)
    {
      return mList.Remove(item);
    }

    public int Count
    {
      get { return mList.Count; }
    }

    public bool IsReadOnly
    {
      get { return mList.IsReadOnly; }
    }

    #endregion

    #region Implementation of IList<long>

    public int IndexOf(long item)
    {
      return mList.IndexOf(item);
    }

    public void Insert(int index, long item)
    {
      mList.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      mList.RemoveAt(index);
    }

    public long this[int index]
    {
      get { return mList[index]; }
      set { mList[index] = value; }
    }

    #endregion

    #region Storage

    private string Filename
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Euler",
                            mName + ".txt");
      }
    }

    private IList<long> LoadFromStore(IEnumerable<long> defaultList)
    {
      if (!File.Exists(Filename))
      {
        return new List<long>(defaultList);
      }

      var list = new List<long>();
      list.AddRange(File.ReadAllLines(Filename).Select(long.Parse));
      return list;
    }

    private void SaveToStore(IEnumerable<long> list)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(Filename));
      File.WriteAllLines(Filename, list.Select(value => value.ToString(CultureInfo.InvariantCulture)));
    }

    #endregion
  }
}