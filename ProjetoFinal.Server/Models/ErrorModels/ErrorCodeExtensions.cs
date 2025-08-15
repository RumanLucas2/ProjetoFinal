using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlMadre.C_.Models
{
    /// <summary>
    /// Enum de Erros do aplicativo
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No Errors
        /// </summary>
        None = 0,

        /// <summary>
        /// String in data format can not have more than 5 characters.
        /// </summary>
        Data5 = 1,

        /// <summary>
        /// String in data format that do not contain a slash (/).
        /// </summary>
        DataNoSlash = 2,

        /// <summary>
        /// Represents an error code indicating that the provided data does not match the expected regular expression
        /// pattern.
        /// </summary>
        DataInvalidRegex = 3,

        /// <summary>
        /// Represents an invalid item state or type.
        /// </summary>
        /// <remarks>This enumeration value is typically used to indicate that an item is invalid or does
        /// not meet the required criteria.</remarks>
        InvalidItem = 4,

        /// <summary>
        /// Represents an error code indicating that the specified item was not found.
        /// </summary>
        ItemNotFound = 5,

        /// <summary>
        /// Represents an error code indicating that the quantity of an item is invalid.
        /// </summary>
        /// <remarks>This error code is typically used to signify that the specified item quantity does
        /// not meet the required constraints, such as being negative or exceeding allowable limits.</remarks>
        ItemQuantityInvalid = 6,

        /// <summary>
        /// Represents an error code indicating that the shopping cart is empty.
        /// </summary>
        EmptyCart = 7,

        /// <summary>
        /// Null Reference Detected.
        /// </summary>
        NullReference = 1000,

        /// <summary>
        /// Represents the HTTP status code for a bad request.
        /// </summary>
        /// <remarks>The status code indicates that the server cannot process the request due to
        /// client-side errors, such as malformed syntax or invalid request parameters.</remarks>
        BadRequest = 400,

        /// <summary>
        /// Represents the HTTP status code for unauthorized access.
        /// </summary>
        /// <remarks>This status code indicates that the request requires user authentication. Typically
        /// returned when the client fails to provide valid credentials.</remarks>
        Unauthorized = 401,

        /// <summary>
        /// Represents the HTTP status code for a forbidden request.
        /// </summary>
        /// <remarks>The status code 403 indicates that the server understood the request but refuses to
        /// authorize it. This typically occurs when the client does not have permission to access the requested
        /// resource.</remarks>
        Forbidden = 403,

        /// <summary>
        /// Represents the HTTP status code for a resource that could not be found.
        /// </summary>
        /// <remarks>This status code indicates that the server could not find the requested resource. It
        /// is commonly used in response to a request for a non-existent endpoint or file.</remarks>
        NotFound = 404,

        /// <summary>
        /// Represents the HTTP status code for a request timeout.
        /// </summary>
        /// <remarks>The status code indicates that the server did not receive a complete request within
        /// the time it was prepared to wait. This is commonly used in scenarios where a client fails to send data
        /// within the expected timeframe.</remarks>
        RequestTimeout = 408,

        /// <summary>
        /// Represents the HTTP status code for an internal server error.
        /// </summary>
        /// <remarks>The status code indicates that the server encountered an unexpected condition that
        /// prevented it from fulfilling the request.</remarks>
        InternalServerError = 500,

        /// <summary>
        /// Represents an error code indicating that a connection to the MongoDB server has failed.
        /// </summary>
        /// <remarks>This error code is typically used to identify issues related to connectivity with a
        /// MongoDB server. It may occur due to network problems, incorrect connection settings, or server
        /// unavailability.</remarks>
        MongoConnectionFailed = 1001,

        /// <summary>
        /// Represents an error code indicating that the provided CPF (Cadastro de Pessoas Físicas) is invalid.
        /// </summary>
        /// <remarks>CPF is a Brazilian individual taxpayer registry identification. This error code is
        /// typically used to  signal validation failures when processing CPF values.</remarks>
        InvalidCpf = 1002,

        /// <summary>
        /// Represents an error code indicating that the provided email address is invalid.
        /// </summary>
        /// <remarks>This error code is typically used to signal validation failures for email addresses.
        /// Ensure that the email address conforms to the expected format before proceeding.</remarks>
        InvalidEmail = 1004,

        /// <summary>
        /// Represents an error code indicating that the provided phone number is invalid.
        /// </summary>
        /// <remarks>This error code can be used to identify validation failures related to phone number
        /// formatting or content. Ensure that the phone number adheres to the expected format and contains valid
        /// characters.</remarks>
        InvalidPhoneNumber = 1005,

        /// <summary>
        /// Represents an error code indicating that the provided CEP (postal code) is invalid.
        /// </summary>
        InvalidCep = 1006,

        /// <summary>
        /// Represents an error code indicating that the provided password is invalid.
        /// </summary>
        /// <remarks>This error code is typically used to signal that a password does not meet the
        /// required criteria or is incorrect during authentication or validation processes.</remarks>
        InvalidPassword = 15,

        /// <summary>
        /// Represents an error code indicating that the specified user could not be found.
        /// </summary>
        UserNotFound = 16,

        /// <summary>
        /// Represents an error code indicating that the user is already logged in.
        /// </summary>
        /// <remarks>This error code is typically used to signal that an attempt to log in was made while
        /// the user is already authenticated.</remarks>
        UserAlreadyLoggedIn = 17,

        /// <summary>
        /// Represents the status code indicating that the user is not paying.
        /// </summary>
        /// <remarks>This status code is typically used to identify scenarios where a user's payment is
        /// overdue or missing. It can be used in payment processing systems or subscription management
        /// workflows.</remarks>
        UserNotPaying = 18,

        /// <summary>
        /// Represents an error code indicating that the specified user access level is invalid.
        /// </summary>
        /// <remarks>This error code is typically used to signal that a provided user access level does
        /// not match any of the valid predefined levels. Ensure that the access level is correctly specified before
        /// retrying the operation.</remarks>
        UserAccessLevelInvalid = 19,

        /// <summary>
        /// Represents the error code for an API timeout condition.
        /// </summary>
        /// <remarks>This error code is used to indicate that an API operation has exceeded the allowed
        /// timeout duration.</remarks>
        APITimeOut = 1007,

        /// <summary>
        /// Represents an error code indicating that the provided API key is invalid.
        /// </summary>
        /// <remarks>This error code is typically returned when an API key fails validation due to being
        /// incorrect, expired, or unauthorized. Ensure that the API key used in the request is valid and properly
        /// configured.</remarks>
        APIKeyInvalid = 1008,

        /// <summary>
        /// Represents the error code indicating that an API key has expired.
        /// </summary>
        /// <remarks>This error code can be used to identify scenarios where an API key is no longer valid
        /// due to expiration. Ensure that the API key is renewed or replaced to continue accessing the API.</remarks>
        APIKeyExpired = 1009,

        /// <summary>
        /// Represents the error code indicating that a required field in the API request is empty.
        /// </summary>
        APIEmptyField = 1010,

        /// <summary>
        /// Represents the force constant with a value of 2001.
        /// </summary>
        /// <remarks>This enumeration value can be used to specify a predefined force constant in
        /// calculations or configurations.</remarks>
        Force1 = 2001,

        /// <summary>
        /// Represents a specific force type with an associated identifier.
        /// </summary>
        /// <remarks>This enumeration value corresponds to the identifier 2002. It can be used to specify
        /// or retrieve a particular force type in relevant operations.</remarks>
        Force2 = 2002,

        /// <summary>
        /// Represents a specific force type with an associated identifier.
        /// </summary>
        /// <remarks>The value <see langword="2003"/> corresponds to the "Force3" type. This enumeration
        /// value can be used to identify or categorize specific forces in the application.</remarks>
        Force3 = 2003,
    }

    public static class ErrorCodeExtensions
    {
        public static string GetMessage(this ErrorCode code)
        {
            return code switch
            {
                //nenhum erro
                ErrorCode.None => "Nenhum erro.",

                //Erro de DataNascimento
                ErrorCode.Data5 => "String não pode ter mais de 5 caracteres",
                ErrorCode.DataNoSlash => "String nao contem \"/\"",
                ErrorCode.DataInvalidRegex => "String falhou na Regex",

                //Erros de item
                ErrorCode.InvalidItem => "Item inválido.",
                ErrorCode.ItemNotFound => "Item não encontrado.",
                ErrorCode.ItemQuantityInvalid => "Quantidade do item inválida.",

                //Erros de Carrinho
                ErrorCode.EmptyCart => "Carrinho vazio.",

                //erros gerais
                ErrorCode.NullReference => "Referência nula detectada.",
                ErrorCode.BadRequest => "Requisição malformada.",
                ErrorCode.Unauthorized => "Acesso não autorizado.",
                ErrorCode.Forbidden => "Você não tem permissão para acessar este recurso.",
                ErrorCode.NotFound => "Recurso não encontrado.",
                ErrorCode.RequestTimeout => "Tempo de requisição esgotado.",
                ErrorCode.InternalServerError => "Erro interno no servidor.",

                //erros de banco de dados
                ErrorCode.MongoConnectionFailed => "Falha ao conectar com o banco de dados.",

                //erros de validação
                ErrorCode.InvalidCpf => "CPF inválido.",
                ErrorCode.InvalidEmail => "Email inválido.",
                ErrorCode.InvalidPhoneNumber => "Número de telefone inválido.",
                ErrorCode.InvalidCep => "CEP inválido.",
                ErrorCode.InvalidPassword => "Senha incorreta.",

                //erros de usuário
                ErrorCode.UserNotFound => "Usuário ou Senha invalidos.",
                ErrorCode.UserAlreadyLoggedIn => "Usuário já está logado.",
                ErrorCode.UserNotPaying => "Usuário nao esta pagando.",
                ErrorCode.UserAccessLevelInvalid => "Nível de acesso do usuário inválido.",

                //erros de API
                ErrorCode.APITimeOut => "Tempo limite da API excedido.",
                ErrorCode.APIKeyInvalid => "Chave da API inválida.",
                ErrorCode.APIKeyExpired => "Chave da API expirada.",
                ErrorCode.APIEmptyField => "Campo obrigatório da API está vazio.",
                ErrorCode.Force1 => "Erro forçado 1: uso exclusivo do desenvolvedor.",
                ErrorCode.Force2 => "Erro forçado 2: uso exclusivo do desenvolvedor.",
                ErrorCode.Force3 => "Erro forçado 3: uso exclusivo do desenvolvedor.",

                //default
                _ => "Erro desconhecido."
            };
        }
    }

    public class AppException : Exception
    {
        public ErrorCode Code { get; }
        #region summary
        /// <summary>
        /// Inicializa uma nova instancia de <see cref="AppException"/> com o codigo de erro especificado
        /// </summary>
        /// <param name="code">O codigo de erro a ser passado</param>
        /// <param name="message">Mensagem opcional</param>
        #endregion
        public AppException(ErrorCode code, string? message = null)
            : base(message ?? code.GetMessage())
        {
            Code = code;
        }
    }

    public class ErrorResult<T>
    {
        public bool Success => Code == ErrorCode.None;
        public ErrorCode Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ErrorResult<T> Ok(T data) =>
            new() { Code = ErrorCode.None, Data = data };

        public static ErrorResult<T> Fail(ErrorCode code, string? message = null) =>
            new()
            {
                Code = code,
                Message = message ?? code.GetMessage()
            };

        public static ErrorResult<T> FromException(Exception ex)
        {
            if (ex is AppException appEx)
            {
                return new ErrorResult<T>
                {
                    Code = appEx.Code,
#if DEBUG
                    Message = $"{appEx.Message}\n\n{appEx.StackTrace}"
#else
                    Message = appEx.Message
#endif
                };
            }

            return new ErrorResult<T>
            {
                Code = ex switch
                {
                    UnauthorizedAccessException => ErrorCode.Unauthorized,
                    ArgumentNullException => ErrorCode.NullReference,
                    TimeoutException => ErrorCode.RequestTimeout,
                    _ => ErrorCode.InternalServerError
                },
#if DEBUG
                Message = $"{ex.Message}\n\n{ex.StackTrace}"
#else
                Message = ex.Message
#endif
            };
        }
    }
}
