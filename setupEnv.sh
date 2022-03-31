#!/bin/sh
cd ActionItemsDashboard

echo  > .env

read -p "Enter your Postgres Server Host Name: "

echo "HOST=$REPLY" > .env

read -p "Enter your Postgres Server Username: "

echo "NAME=$REPLY" >> .env

read -p "Enter your Postgres Server Password: "

echo "PASSWORD=$REPLY" >> .env

read -p "Enter your Postgres Server Database Name: "

echo "DATABASE=$REPLY" >> .env
