namespace Practice_wasm.Utils
{

    public class Result<T>
    {
        public T Value { get; set; }
        public string message { get; set; }
        public bool IsSuccess { get; set; }

        // Constructor para un resultado exitoso
        public static Result<T> Success(T value, string? _message)
        {
            return new Result<T>
            {
                Value = value,
                IsSuccess = true,
                message = _message // Mensaje de éxito opcional
            };
        }

        // Constructor para un resultado fallido
        public static Result<T> Failure(string _message)
        {
            return new Result<T>
            {
                Value = default(T), // Valor por defecto para el tipo genérico
                IsSuccess = false,
                message = _message // Mensaje de error
            };
        }

    }
}