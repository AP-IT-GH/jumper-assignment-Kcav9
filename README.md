# Jumping Taxi

## Inleiding

Via deze tutorial proberen wij u stapsgewijs te gidsen door ons Machine Learning project. We maakten gebruik van Unity, Anaconda en de ML-Agents package om een taxi te kunnen leren springen over politiewagens.
Heel veel plezier met het uitvoeren van deze opdracht!

![image](https://user-images.githubusercontent.com/73060860/145276168-179004ce-8633-4630-8438-68e651c2a230.png)


## Spelverloop

Wanneer u de applicatie start zal u zien dat een taxi centraal in beeld zal staan. Deze taxi is de agent en krijgt de opdracht om over regelmatig aanrijdende politiewagens te springen. De politiewagens noemen we de vijanden of enemies en het is dus de bedoeling dat de taxi deze niet raakt. Boven deze politiewagens zal daarnaast ook een pak friet en een cola te zien zijn die de taxi moet trachten te vangen. Via Machine Learning zullen we deze doelen proberen te bereiken.

## Observaties

De agent of taxi kan 2 situaties observeren:

1. Een naderende politiewagen
2. Geen naderende politiewagen

## Observaties

De agent of taxi kan 2 acties uitvoeren om te reageren op de observaties:

1. Springen
2. Niet springen

## Beloning

Als we nu alle mogelijke situaties bekijken die zich kunnen voordoen dan kunnen we volgend beloningssysteem hanteren:

|        Observaties       |      Acties     |  Beloning |
|------------------------- |:---------------:|----------:|
|  Naderende politiewagen  |     Springen    |   +0.1f   |
|  Naderende politiewagen  |  Niet springen  |   -1.0f   |
|          Niets           |     Springen    |   -0.2f   |




