using System;
using System.Runtime.InteropServices.ComTypes;

namespace CircularBuffer
{
    public class SimpleCircularBuffer<T>
    {
        private T[] _buffer;

        private int _size;

        private int _index = 0;

        private readonly int _capacity;

        public SimpleCircularBuffer(int capacity)
        {
            _buffer = new T[capacity];
            _capacity = capacity;
        }

        public void Push(T value)
        {
            // add index
            _buffer[_index] = value;
            
            // update index
            _index = (_index + 1) % _capacity;

            // update size
            if (_size != _capacity)
                _size++;
        }

        public T[] ToArray()
        {
            var newArray = new T[_size];

            for (var i = 0; i < _size; i++)
            {
                var index = Mod(_index - 1 - i, _capacity);
                newArray[i] = _buffer[index];
            }

            return newArray;
        }

        private static int Mod(int k, int n) {  return ((k %= n) < 0) ? k+n : k;  }
    }
}