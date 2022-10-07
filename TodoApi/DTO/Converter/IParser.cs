namespace TodoApi.DTO.Converter
{
   public interface IParser<O, D>
   {
      D Parse(O origin);
      IEnumerable<D> Parse(IEnumerable<O> origin);
   }
}
