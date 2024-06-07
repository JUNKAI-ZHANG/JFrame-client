using System;

namespace Script.CSharp.Lib.Utils
{
    public class RingBuffer {
        private byte[] _buffer;
        private uint _begin;
        private uint _end;
        private uint _remain;
        private uint _capacity;

        private const int MAX_BUFFER_SIZE = 1024 * 1024;

        public RingBuffer() {
            _buffer = new byte[MAX_BUFFER_SIZE];
            _begin = _end = _capacity = 0;
            _remain = MAX_BUFFER_SIZE;
        }

        public void Dispose() {
            _begin = _end = _capacity = 0;
            _remain = MAX_BUFFER_SIZE;
            Array.Clear(_buffer, 0, _buffer.Length);
        }

        public bool AddBuffer(byte[] buffer, uint size) {
            if (size > _remain) {
                return false;
            }

            for (uint i = 0; i < size; i++) {
                _buffer[_end] = buffer[i];
                _end = (_end + 1) % MAX_BUFFER_SIZE;
            }

            _capacity += size;
            _remain -= size;

            return true;
        }

        public bool PopBuffer(uint size) {
            if (size > _capacity) {
                return false;
            }

            _begin = (_begin + size) % MAX_BUFFER_SIZE;

            _capacity -= size;
            _remain += size;
            return true;
        }

        public byte[] GetBuffer(uint len) {
            uint start = _begin;
            byte[] ret = new byte[len];

            for (int i = 0; i < len; i++) {
                ret[i] = _buffer[start];
                start = (start + 1) % MAX_BUFFER_SIZE;
            }

            return ret;
        }

        public uint GetRemain() {
            return _remain;
        }

        public uint GetCapacity() {
            return _capacity;
        }

        public byte this[int id] {
            get {
                if (id < 0 || id >= MAX_BUFFER_SIZE) {
                    return 0;
                }
                return _buffer[id];
            }
        }
    }
}