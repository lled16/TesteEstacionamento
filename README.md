
# ParkingAPI

ParkingAPI trata-se de uma API para controle de um estacionamento. Abaixo estão suas funções.

1. Retorna quantas vagas vazias restam

2. Retorna quantas vagas totais há no estacionamento

3. Retorna se o estacionamento está cheio e se está vazio

4. Retorna se um determinado grupo de vagas está totalmente ocupado, ex: Todas as vagas de motos estão ocupadas ? 

5. Retorna quantas vagas as vans estão ocupando
- Contabilizando mesmo que tenham vans estacionadas em vagas de carros, realizandoa validação pela real quantidade pela placa.

6. Retorna todos os veículos estacionados, trazendo o número da vaga, sua placa e seu tipo. 

7. Permite a ação de estacionar um veículo (Moto, Carro ou Van) verificando se as respectivas vagas estão disponíveis e assim realizando a inserção seguindo a regra : 

- Uma moto pode estacionar em qualquer lugar
- Um carro pode estacionar em uma única vaga para carro, ou em uma vaga grande
- Uma van pode estacionar, mas ocupará 3 vagas de carro, ou uma vaga grande
- Realiza validação da placa digitada se está no modelo comum ou mercosul

Realiza a inserção do veículo na respectiva vaga, verificando se o mesmo já existe no estacionamento.

8. Permite a remoção de um veículo pela placa do mesmo.
- Realiza validação se o mesmo de fato existe.
- Realiza validação da placa digitada se está no modelo comum ou mercosul

9. Permite a remoção de todos os veículos de uma vez.




 #### **Por padrão, o estacionamento possui apenas 21 vagas para serem utilizadas .** 
 #### **Para agilizar a entrega, não houve implementação da camada Infrastructure, bem como conexão com banco ou EF.**


## Swagger

