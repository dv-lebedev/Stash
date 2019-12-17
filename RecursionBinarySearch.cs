/*
The MIT License (MIT)
Copyright (c) 2019 Denis Lebedev
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;

public class RecursionBinarySearch
{
    public static int Search<T, R>(T[] array, R value, Func<T, R> selector) 
        where R : IComparable
    {
        return Search(array, value, selector, 0, array.Length);
    }

    private static int Search<T, R>(T[] array, R value, Func<T, R> selector, int first, int last, int prevmid = 0) 
        where R : IComparable
    {
        int mid = (first + last) / 2;

        if (mid == prevmid) return -1; //if item not found

        R arrMid = selector(array[mid]);

        int res = value.CompareTo(arrMid);

        if (res == 0) return mid;
        else if (res == 1) first = mid;
        else if (res == -1) last = mid;

        return Search(array, value, selector, first, last, mid);
    }
}
