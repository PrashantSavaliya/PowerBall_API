using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;
using System.Data;

namespace PowerBall.Repository
{
    public class DataListRepo : IDataListRepo
    {
        private readonly DataContext _context;
        private readonly string _connectionString;

        public DataListRepo(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DBCS");
        }

        public async Task<List<DataList>> GetAllData()
        {
            try
            {
                var data = await _context.DataList.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting data", ex);
            }
        }

        public async Task<List<DataList>> GetDataByPage(int pageIndex, int pageSize)
        {
            try
            {
                var data = await _context.DataList
                                          .Skip((pageIndex - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting data", ex);
            }
        }


        public async Task<List<DataList>> GetDataBY_SP()
        {
            List<DataList> dataList = new List<DataList>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("GetAllData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DataList data = new DataList();
                                data.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                data.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                                data.Name = reader.GetString(reader.GetOrdinal("Name"));
                                data.TicketId = reader.GetInt32(reader.GetOrdinal("TicketId"));
                                data.DrawId = reader.GetInt32(reader.GetOrdinal("DrawId"));
                                data.BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                                data.TotalAmount = reader.GetDouble(reader.GetOrdinal("TotalAmount"));
                                dataList.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while executing the stored procedure: {ex.Message}");
            }

            return dataList;
        }


        public async Task<List<DataList>> GetDataByPageSP(int pageNumber, int pageSize)
        {
            List<DataList> dataList = new List<DataList>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("GetAllDataPaged", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PageNumber", pageNumber);
                        command.Parameters.AddWithValue("@PageSize", pageSize);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DataList data = new DataList();
                                data.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                data.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                                data.Name = reader.GetString(reader.GetOrdinal("Name"));
                                data.TicketId = reader.GetInt32(reader.GetOrdinal("TicketId"));
                                data.DrawId = reader.GetInt32(reader.GetOrdinal("DrawId"));
                                data.BarCode = reader.GetString(reader.GetOrdinal("BarCode"));
                                data.TotalAmount = reader.GetDouble(reader.GetOrdinal("TotalAmount"));
                                dataList.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while executing the stored procedure: {ex.Message}");
            }

            return dataList;
        }

    }
}
