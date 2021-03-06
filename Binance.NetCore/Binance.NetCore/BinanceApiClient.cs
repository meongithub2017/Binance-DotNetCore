﻿using Binance.NetCore.Data;
using Binance.NetCore.Data.Interface;
using Binance.NetCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Binance.NetCore
{
    public class BinanceApiClient
    {
        [Obsolete("This initializer is depricated. Use direct method calls instead.")]
        public IBinanceRepository BinanceRepository;
        private IBinanceRepository _repository;

        /// <summary>
        /// Constructor - no authentication
        /// </summary>
        public BinanceApiClient()
        {
            BinanceRepository = new BinanceRepository();
            _repository = new BinanceRepository();
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        public BinanceApiClient(string apiKey, string apiSecret)
        {
            BinanceRepository = new BinanceRepository(apiKey, apiSecret);
            _repository = new BinanceRepository(apiKey, apiSecret);
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="configPath">Path to config file</param>
        public BinanceApiClient(string configPath)
        {
            BinanceRepository = new BinanceRepository(configPath);
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
        /// Place a limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse LimitOrder(string symbol, Side side, decimal quantity, decimal price)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = TimeInForce.GTC.ToString(),
                type = OrderType.LIMIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a limit maker order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse LimitMakerOrder(string symbol, Side side, decimal quantity, decimal price)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                type = OrderType.LIMIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse LimitOrder(string symbol, Side side, decimal quantity, decimal price, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.LIMIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a market order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse MarketOrder(string symbol, Side side, decimal quantity)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                type = OrderType.MARKET.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a market order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="timeInForce">Time in force</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse MarketOrder(string symbol, Side side, decimal quantity, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.MARKET.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a stop loss
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse StopLoss(string symbol, Side side, decimal quantity, decimal stopPrice)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                type = OrderType.STOP_LOSS.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a stop loss limit
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <param name="timeInForce">Time in Force</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse StopLossLimit(string symbol, Side side, decimal quantity, decimal price, decimal stopPrice, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.STOP_LOSS_LIMIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a take profit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse TakeProfit(string symbol, Side side, decimal quantity, decimal stopPrice)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                type = OrderType.TAKE_PROFIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Place a take profit limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <param name="timeInForce">Time in Force</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse TakeProfitLimit(string symbol, Side side, decimal quantity, decimal price, decimal stopPrice, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.TAKE_PROFIT_LIMIT.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
        }

        /// <summary>
        /// Post/Place a trade
        /// </summary>
        /// <param name="tradeParams">Trade to place</param>
        /// <returns>TradeResponse object</returns>
        public TradeResponse PostTrade(string symbol, Side side, decimal quantity, decimal price, OrderType type, TimeInForce timeInForce )
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = type.ToString()
            };

            return _repository.PostTrade(tradeParams).Result;
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
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="amount">Decimal of amount</param>
        /// <returns>Withdrawal response</returns>
        public WithdrawalResponse WithdrawFunds(string symbol, string address, decimal amount)
        {
            return _repository.WithdrawFunds(symbol, address, amount).Result;
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="amount">Decimal of amount</param>
        /// <param name="description">Description of address</param>
        /// <returns>Withdrawal response</returns>
        public WithdrawalResponse WithdrawFunds(string symbol, string address, decimal amount, string description)
        {
            return _repository.WithdrawFunds(symbol, address, amount,description).Result;
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="addressTag">Secondary address identifier</param>
        /// <param name="amount">Decimal of amount</param>
        /// <returns>Withdrawal response</returns>
        public WithdrawalResponse WithdrawFunds(string symbol, string address, string addressTag, decimal amount)
        {
            return _repository.WithdrawFunds(symbol, address, addressTag, amount).Result;
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="addressTag">Secondary address identifier</param>
        /// <param name="amount">Decimal of amount</param>
        /// <param name="description">Description of address</param>
        /// <returns>Withdrawal response</returns>
        public WithdrawalResponse WithdrawFunds(string symbol, string address, string addressTag, decimal amount, string description)
        {
            return _repository.WithdrawFunds(symbol, address, addressTag, amount, description).Result;
        }

        /// <summary>
        /// Get all deposit history
        /// </summary>
        /// <param name="status">deposit status (default all)</param>
        /// <returns>Array of deposits</returns>
        public Deposit[] GetDepositHistory(DepositStatus status = DepositStatus.all)
        {
            return _repository.GetDepositHistory(status).Result;
        }

        /// <summary>
        /// Get deposit history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">deposit status (default all)</param>
        /// <returns>Array of deposits</returns>
        public Deposit[] GetDepositHistory(string asset, DepositStatus status = DepositStatus.all)
        {
            return _repository.GetDepositHistory(asset, status).Result;
        }

        /// <summary>
        /// Get deposit history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">deposit status (default all)</param>
        /// <param name="startTime">Start of date range</param>
        /// <param name="endTime">End of date range</param>
        /// <returns>Array of deposits</returns>
        public Deposit[] GetDepositHistory(string asset, DepositStatus status, DateTime startTime, DateTime endTime)
        {
            return _repository.GetDepositHistory(asset, status, startTime, endTime).Result;
        }

        /// <summary>
        /// Get all withdrawal history
        /// </summary>
        /// <param name="status">withdrawal status (default all)</param>
        /// <returns>Array of withdrawal</returns>
        public Withdrawal[] GetWithdrawalHistory(WithdrawalStatus status = WithdrawalStatus.all)
        {
            return _repository.GetWithdrawalHistory(status).Result;
        }

        /// <summary>
        /// Get withdrawal history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">withdrawal status (default all)</param>
        /// <returns>Array of withdrawal</returns>
        public Withdrawal[] GetWithdrawalHistory(string asset, WithdrawalStatus status = WithdrawalStatus.all)
        {
            return _repository.GetWithdrawalHistory(asset, status).Result;
        }

        /// <summary>
        /// Get withdrawal history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">withdrawal status (default all)</param>
        /// <param name="startTime">Start of date range</param>
        /// <param name="endTime">End of date range</param>
        /// <returns>Array of withdrawal</returns>
        public Withdrawal[] GetWithdrawalHistory(string asset, WithdrawalStatus status, DateTime startTime, DateTime endTime)
        {
            return _repository.GetWithdrawalHistory(asset, status, startTime, endTime).Result;
        }

        /// <summary>
        /// Get deposit address for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <returns>String of address</returns>
        public Dictionary<string, string> GetDepositAddress(string asset)
        {
            return _repository.GetDepositAddress(asset).Result;
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
        /// Place a limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> LimitOrderAsync(string symbol, Side side, decimal quantity, decimal price)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = TimeInForce.GTC.ToString(),
                type = OrderType.LIMIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a limit maker order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> LimitMakerOrderAsync(string symbol, Side side, decimal quantity, decimal price)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                type = OrderType.LIMIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> LimitOrderAsync(string symbol, Side side, decimal quantity, decimal price, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.LIMIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a market order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> MarketOrderAsync(string symbol, Side side, decimal quantity)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                type = OrderType.MARKET.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a market order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="timeInForce">Time in force</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> MarketOrderAsync(string symbol, Side side, decimal quantity, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.MARKET.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a stop loss
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> StopLossAsync(string symbol, Side side, decimal quantity, decimal stopPrice)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                type = OrderType.STOP_LOSS.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a stop loss limit
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <param name="timeInForce">Time in Force</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> StopLossLimitAsync(string symbol, Side side, decimal quantity, decimal price, decimal stopPrice, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.STOP_LOSS_LIMIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a take profit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> TakeProfitAsync(string symbol, Side side, decimal quantity, decimal stopPrice)
        {
            var tradeParams = new TradeParams
            {
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                type = OrderType.TAKE_PROFIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Place a take profit limit order
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="side">Side of trade (Buy/Sell)</param>
        /// <param name="quantity">Decimal of quantity</param>
        /// <param name="price">Decimal of price</param>
        /// <param name="stopPrice">Decimal of stop price</param>
        /// <param name="timeInForce">Time in Force</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> TakeProfitLimitAsync(string symbol, Side side, decimal quantity, decimal price, decimal stopPrice, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                stopPrice = stopPrice,
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = OrderType.TAKE_PROFIT_LIMIT.ToString()
            };

            return await _repository.PostTrade(tradeParams);
        }

        /// <summary>
        /// Post/Place a trade
        /// </summary>
        /// <param name="tradeParams">Trade to place</param>
        /// <returns>TradeResponse object</returns>
        public async Task<TradeResponse> PostTradeAsync(string symbol, Side side, decimal quantity, decimal price, OrderType type, TimeInForce timeInForce)
        {
            var tradeParams = new TradeParams
            {
                price = price,
                quantity = quantity,
                side = side.ToString(),
                symbol = symbol,
                timeInForce = timeInForce.ToString(),
                type = type.ToString()
            };

            return await _repository.PostTrade(tradeParams);
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

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="amount">Decimal of amount</param>
        /// <returns>Withdrawal response</returns>
        public async Task<WithdrawalResponse> WithdrawFundsAsync(string symbol, string address, decimal amount)
        {
            return await _repository.WithdrawFunds(symbol, address, amount);
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="amount">Decimal of amount</param>
        /// <param name="description">Description of address</param>
        /// <returns>Withdrawal response</returns>
        public async Task<WithdrawalResponse> WithdrawFundsAsync(string symbol, string address, decimal amount, string description)
        {
            return await _repository.WithdrawFunds(symbol, address, amount, description);
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="addressTag">Secondary address identifier</param>
        /// <param name="amount">Decimal of amount</param>
        /// <returns>Withdrawal response</returns>
        public async Task<WithdrawalResponse> WithdrawFundsAsync(string symbol, string address, string addressTag, decimal amount)
        {
            return await _repository.WithdrawFunds(symbol, address, addressTag, amount);
        }

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of asset</param>
        /// <param name="address">Address to send funds to</param>
        /// <param name="addressTag">Secondary address identifier</param>
        /// <param name="amount">Decimal of amount</param>
        /// <param name="description">Description of address</param>
        /// <returns>Withdrawal response</returns>
        public async Task<WithdrawalResponse> WithdrawFundsAsync(string symbol, string address, string addressTag, decimal amount, string description)
        {
            return await _repository.WithdrawFunds(symbol, address, addressTag, amount, description);
        }

        /// <summary>
        /// Get all deposit history
        /// </summary>
        /// <param name="status">deposit status (default all)</param>
        /// <returns>Array of deposits</returns>
        public async Task<Deposit[]> GetDepositHistoryAsync(DepositStatus status = DepositStatus.all)
        {
            return await _repository.GetDepositHistory(status);
        }

        /// <summary>
        /// Get deposit history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">deposit status (default all)</param>
        /// <returns>Array of deposits</returns>
        public async Task<Deposit[]> GetDepositHistoryAsync(string asset, DepositStatus status = DepositStatus.all)
        {
            return await _repository.GetDepositHistory(asset, status);
        }

        /// <summary>
        /// Get deposit history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">deposit status (default all)</param>
        /// <param name="startTime">Start of date range</param>
        /// <param name="endTime">End of date range</param>
        /// <returns>Array of deposits</returns>
        public async Task<Deposit[]> GetDepositHistoryAsync(string asset, DepositStatus status, DateTime startTime, DateTime endTime)
        {
            return await _repository.GetDepositHistory(asset, status, startTime, endTime);
        }

        /// <summary>
        /// Get all withdrawal history
        /// </summary>
        /// <param name="status">withdrawal status (default all)</param>
        /// <returns>Array of withdrawal</returns>
        public async Task<Withdrawal[]> GetWithdrawalHistoryAsync(WithdrawalStatus status = WithdrawalStatus.all)
        {
            return await _repository.GetWithdrawalHistory(status);
        }

        /// <summary>
        /// Get withdrawal history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">withdrawal status (default all)</param>
        /// <returns>Array of withdrawal</returns>
        public async Task<Withdrawal[]> GetWithdrawalHistoryAsync(string asset, WithdrawalStatus status = WithdrawalStatus.all)
        {
            return await _repository.GetWithdrawalHistory(asset, status);
        }

        /// <summary>
        /// Get withdrawal history for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <param name="status">withdrawal status (default all)</param>
        /// <param name="startTime">Start of date range</param>
        /// <param name="endTime">End of date range</param>
        /// <returns>Array of withdrawal</returns>
        public async Task<Withdrawal[]> GetWithdrawalHistoryAsync(string asset, WithdrawalStatus status, DateTime startTime, DateTime endTime)
        {
            return await _repository.GetWithdrawalHistory(asset, status, startTime, endTime);
        }

        /// <summary>
        /// Get deposit address for an asset
        /// </summary>
        /// <param name="asset">string of asset</param>
        /// <returns>String of address</returns>
        public async Task<Dictionary<string, string>> GetDepositAddressAsync(string asset)
        {
            return await _repository.GetDepositAddress(asset);
        }
    }
}
