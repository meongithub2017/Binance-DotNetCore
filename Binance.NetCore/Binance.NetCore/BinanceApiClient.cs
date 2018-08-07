﻿using Binance.NetCore.Data;
using Binance.NetCore.Data.Interface;
using Binance.NetCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.NetCore
{
    public class BinanceApiClient
    {
        private IBinanceRepository _repository;

        /// <summary>
        /// Constructor - no authentication
        /// </summary>
        public BinanceApiClient()
        {
            _repository = new BinanceRepository();
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        public BinanceApiClient(string apiKey, string apiSecret)
        {
            _repository = new BinanceRepository(apiKey, apiSecret);
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="configPath">Path to config file</param>
        public BinanceApiClient(string configPath)
        {
            _repository = new BinanceRepository(configPath);
        }

        /// <summary>
        /// Check if the Exchange Repository is ready for trading
        /// </summary>
        /// <returns>Boolean of validation</returns>
        public bool ValidateExchangeConfigured()
        {
            return _repository.ValidateExchangeConfigured();
        }

        /// <summary>
        /// Get Transactions for account
        /// </summary>
        /// <returns>Collection of Transactions</returns>
        public IEnumerable<Transaction> GetTransactions()
        {
            return _repository.GetTransactions().Result;
        }

        /// <summary>
        /// Get account balance
        /// </summary>
        /// <returns>Account object</returns>
        public Account GetBalance()
        {
            return _repository.GetBalance().Result;
        }

        /// <summary>
        /// Get order information
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <param name="orderId">long of orderId</param>
        /// <returns>OrderResponse object</returns>
        public OrderResponse GetOrder(string symbol, long orderId)
        {
            return _repository.GetOrder(symbol, orderId).Result;
        }

        /// <summary>
        /// Get all order information
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <param name="limit">Int of orders count to return, default 20</param>
        /// <returns>Array OrderResponse object</returns>
        public OrderResponse[] GetOrders(string symbol, int limit = 20)
        {
            return _repository.GetOrders(symbol, limit).Result;
        }

        /// <summary>
        /// Get all open orders
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <returns>Array OrderResponse object</returns>
        public OrderResponse[] GetOpenOrders(string symbol)
        {
            return _repository.GetOpenOrders(symbol).Result;
        }

        /// <summary>
        /// Get Order Book for a pair
        /// </summary>
        /// <param name="symbol">string of trading pair</param>
        /// <param name="limit">Number of orders to return</param>
        /// <returns>OrderBook object</returns>
        public OrderBook GetOrderBook(string symbol, int limit = 100)
        {
            return _repository.GetOrderBook(symbol, limit).Result;
        }

        /// <summary>
        /// Post/Place a trade
        /// </summary>
        /// <param name="tradeParams">Trade to place</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse PostTrade(TradeParams tradeParams)
        {
            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Delete/Cancel a trade
        /// </summary>
        /// <param name="tradeParams">Trade to delete</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse DeleteTrade(CancelTradeParams tradeParams)
        {
            return _repository.DeleteTrade(tradeParams).Result;
        }

        /// <summary>
        /// Get Ticker for all pairs
        /// </summary>
        /// <returns>Collection of BinanceTick objects</returns>
        public IEnumerable<Tick> GetCrytpos()
        {
            return _repository.GetCrytpos().Result;
        }

        /// <summary>
        /// Get Candlesticks for a symbol
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="interval">Time interval</param>
        /// <param name="limit">Time limit</param>
        /// <returns>Array of Candlestick objects</returns>
        public Candlestick[] GetCandlestick(string symbol, Interval interval, int limit = 500)
        {
            return _repository.GetCandlestick(symbol, interval, limit).Result;
        }

        /// <summary>
        /// Get 24hour ticker statistics
        /// </summary>
        /// <param name="symbol">Trading symbol (default = "")</param>
        /// <returns>Array of Tick objects</returns>
        public Tick[] Get24HourStats(string symbol = "")
        {
            return _repository.Get24HourStats(symbol).Result;
        }

        /// <summary>
        /// Get BinanceTime
        /// </summary>
        /// <returns>long of timestamp</returns>
        public long GetBinanceTime()
        {
            return _repository.GetBinanceTime();
        }

        /// <summary>
        /// Get Transactions for account Async
        /// </summary>
        /// <returns>Collection of Transactions</returns>
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _repository.GetTransactions();
        }

        /// <summary>
        /// Get account balance Async
        /// </summary>
        /// <returns>Account object</returns>
        public async Task<Account> GetBalanceAsync()
        {
            return await _repository.GetBalance();
        }

        /// <summary>
        /// Get order information Async
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <param name="orderId">long of orderId</param>
        /// <returns>OrderResponse object</returns>
        public async Task<OrderResponse> GetOrderAsync(string symbol, long orderId)
        {
            return await _repository.GetOrder(symbol, orderId);
        }

        /// <summary>
        /// Get all order information Async
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <param name="limit">Int of orders count to return, default 20</param>
        /// <returns>Array OrderResponse object</returns>
        public async Task<OrderResponse[]> GetOrdersAsync(string symbol, int limit = 20)
        {
            return await _repository.GetOrders(symbol, limit);
        }

        /// <summary>
        /// Get all open orders Async
        /// </summary>
        /// <param name="symbol">string of symbol</param>
        /// <returns>Array OrderResponse object</returns>
        public async Task<OrderResponse[]> GetOpenOrdersAsync(string symbol)
        {
            return await _repository.GetOpenOrders(symbol);
        }

        /// <summary>
        /// Get Order Book for a pair Async
        /// </summary>
        /// <param name="symbol">string of trading pair</param>
        /// <param name="limit">Number of orders to return</param>
        /// <returns>OrderBook object</returns>
        public async Task<OrderBook> GetOrderBookAsync(string symbol, int limit = 100)
        {
            return await _repository.GetOrderBook(symbol, limit);
        }

        /// <summary>
        /// Post/Place a trade Async
        /// </summary>
        /// <param name="tradeParams">Trade to place</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> PostTradeAsync(TradeParams tradeParams)
        {
            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Delete/Cancel a trade Async
        /// </summary>
        /// <param name="tradeParams">Trade to delete</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> DeleteTradeAsync(CancelTradeParams tradeParams)
        {
            return await _repository.DeleteTrade(tradeParams);
        }

        /// <summary>
        /// Get Ticker for all pairs Async
        /// </summary>
        /// <returns>Collection of BinanceTick objects</returns>
        public async Task<IEnumerable<Tick>> GetCrytposAsync()
        {
            return await _repository.GetCrytpos();
        }

        /// <summary>
        /// Get Candlesticks for a symbol Async
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="interval">Time interval</param>
        /// <param name="limit">Time limit</param>
        /// <returns>Array of Candlestick objects</returns>
        public async Task<Candlestick[]> GetCandlestickAsync(string symbol, Interval interval, int limit = 500)
        {
            return await _repository.GetCandlestick(symbol, interval, limit);
        }

        /// <summary>
        /// Get 24hour ticker statistics Async
        /// </summary>
        /// <param name="symbol">Trading symbol (default = "")</param>
        /// <returns>Array of Tick objects</returns>
        public async Task<Tick[]> Get24HourStatsAsync(string symbol = "")
        {
            return await _repository.Get24HourStats(symbol);
        }
    }
}
