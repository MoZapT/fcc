using Dapper;
using Shared.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessInfrastructure.Repositories
{
    public class SqlBaseRepository : ISqlBaseRepository
    {
        private readonly string _fccConStr = ConfigurationManager.ConnectionStrings["FccConStr"].ConnectionString;
        private readonly Dictionary<int, SqlConnection> _conSet;

        public string FccConStr
        {
            get { return _fccConStr; }
        }

        public SqlBaseRepository()
        {
            _conSet = new Dictionary<int, SqlConnection>();
        }

        public async Task<T> QueryFoD<T>(string query)
        {

            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.QueryFirstOrDefaultAsync<T>(query);
        }

        public async Task<T> QueryFoD<T>(string query, object parameters)
        {
            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> Query<T>(string query)
        {
            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.QueryAsync<T>(query);
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object parameters)
        {
            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.QueryAsync<T>(query, parameters);
        }

        public async Task<int> Execute(string query, object parameters)
        {
            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.ExecuteAsync(query, parameters);
        }

        public async Task<T> ExecuteScalar<T>(string query, object parameters)
        {
            var tId = Convert.ToInt32(Task.CurrentId);
            var tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;

            if (tCon == null)
            {
                _conSet.Add(tId, new SqlConnection(_fccConStr));
            }

            tCon = _conSet.ContainsKey(tId) ? _conSet[tId] : null;
            return await tCon.ExecuteScalarAsync<T>(query, parameters);
        }

        public bool Transaction(Task task)
        {
            var success = false;

            using (var transactionScope = new TransactionScope())
            {
                using (var con = new SqlConnection(_fccConStr))
                {
                    con.Open();

                    using (var tcn = con.BeginTransaction())
                    {
                        try
                        {
                            //  set connection to conSet                        
                            _conSet.Add(task.Id, con);

                            //  start the task
                            task.Start();

                            //  wait to completion
                            Task.WaitAll(task);

                            //  complete or rollback
                            tcn.Commit();

                            success = true;
                        }
                        catch (Exception ex)
                        {
                            tcn.Rollback();
                            success = false;
                        }
                        finally
                        {
                            //  complete or rollback
                            transactionScope.Complete();

                            con.Close();

                            if (_conSet.ContainsKey(task.Id))
                            {
                                _conSet.Remove(task.Id);
                            }

                            task.Dispose();
                        }
                    }
                }
            }

            return success;
        }

        public bool Transaction(Task task, SqlConnection con)
        {
            var success = false;
            using (var transactionScope = new TransactionScope())
            {
                con.Open();

                using (var tcn = con.BeginTransaction())
                {
                    try
                    {
                        //  set connection to conSet                        
                        _conSet.Add(task.Id, con);

                        //  start the task
                        task.Start();

                        //  wait to completion
                        Task.WaitAll(task);

                        //  transaction commit
                        tcn.Commit();

                        success = true;
                    }
                    catch (Exception ex)
                    {
                        tcn.Rollback();
                        con.Close();
                        success = false;
                    }

                    con.Close();
                }

                return success;
            }
        }
    }
}