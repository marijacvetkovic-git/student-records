using student_records.Business.DTOs.JWT;

namespace student_records.Business.TokenGenerator
{
    public interface ITokenGenerator
    {
        JWTResultsDTO GenerateJWTToken(JWTCreateDTO user, DateTime expires);
    }
}