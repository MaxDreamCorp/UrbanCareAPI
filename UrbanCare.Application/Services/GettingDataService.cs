using UrbanCare.Application.DTOs.Responses;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.Application.Services
{
    public class GettingDataService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IManagementCompanyRepository _managementCompanyRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IOrderRepository _orderRepository;

        public GettingDataService(IBuildingRepository buildingRepository,
                                  IRegionRepository regionRepository,
                                  IManagementCompanyRepository managementCompanyRepository,
                                  IApartmentRepository apartmentRepository,
                                  IResidentRepository residentRepository,
                                  IOrderRepository orderRepository)
        {
            _buildingRepository = buildingRepository;
            _regionRepository = regionRepository;
            _managementCompanyRepository = managementCompanyRepository;
            _apartmentRepository = apartmentRepository;
            _residentRepository = residentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<BuildingResponseDTO> GetBuildingResponseDTOByBuildingIdAsync(int buildingId, CancellationToken cancellationToken = default)
        {
            var building = await _buildingRepository.GetByIdAsync(buildingId, cancellationToken);
            if (building == null)
                throw new Exception("Данного здания не существует");

            var region = await _regionRepository.GetByIdAsync(building.RegionId);

            if (region == null)
                throw new Exception("Данного региона не существует");

            var managementCompany = await _managementCompanyRepository.GetByIdAsync(region.ManagementCompanyId, cancellationToken);

            if (managementCompany == null)
                throw new Exception("Данной УК не существует");

            var managementCompanyDTO = new ManagementCompanyResponseDTO(
                managementCompany.Id,
                managementCompany.Name,
                managementCompany.Address);

            var regionDTO = new RegionResponseDTO(region.Id,
                        region.Name,
                        region.CommonAddress,
                        managementCompanyDTO);

            return new BuildingResponseDTO(
                    building.Id,
                    building.Number,
                    building.Address,
                    regionDTO,
                    new BuildingTypeResponseDTO(
                        building.BuildingType.Id,
                        building.BuildingType.Type),
                    building.YearBuilt,
                    building.FloorCount,
                    new WallMaterialResponseDTO(
                        building.WallMaterial.Id,
                        building.WallMaterial.Name),
                    new FloorMaterialResponseDTO(
                        building.FloorMaterial.Id,
                        building.FloorMaterial.Name));
        }

        public async Task<ApartmentResponseDTO> GetApartmentResponseDTOByApartmentIdAsync(int apartmentId, CancellationToken cancellationToken = default)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(apartmentId, cancellationToken);

            if (apartment == null)
                throw new Exception("Данной квартиры не существует");

            var buildingDTO = await GetBuildingResponseDTOByBuildingIdAsync(apartment.BuildingId, cancellationToken);
            bool isFree = await _apartmentRepository.IsFreeByIdAsync(apartment.Id, cancellationToken);

            return new ApartmentResponseDTO(
                apartment.Id,
                apartment.Number,
                buildingDTO,
                apartment.Entrance,
                apartment.Floor,
                apartment.RoomsCount,
                isFree);
        }

        public async Task<ResidentResponseDTO> GetResidentResponseDTOByResidentIdAsync(int residentId, CancellationToken cancellationToken = default)
        {
            var resident = await _residentRepository.GetByIdAsync(residentId, cancellationToken);

            if (resident == null)
                throw new Exception("Данного жителя не существует");

            var apartmentDTO = await GetApartmentResponseDTOByApartmentIdAsync(resident.ApartmentId, cancellationToken);

            return new ResidentResponseDTO(
                 resident.Id,
                 new(
                     resident.User.Id,
                     resident.User.Fullname,
                     resident.User.Email,
                     resident.User.Phone,
                     resident.User.RoleId,
                     resident.User.UserPersonalData.DateOfBirth),
                 apartmentDTO,
                 resident.MovingIntoDate,
                 resident.MovingOutDate,
                 resident.IsLiving == 1);
        }

        public async Task<OrderResponseDTO> GetOrderResponseDTOByOrderIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

            if (order == null)
                throw new Exception("Данного заказа не существует");

            var residentDTO = await GetResidentResponseDTOByResidentIdAsync(order.ResidentId, cancellationToken);

            var buildingDTO = await GetBuildingResponseDTOByBuildingIdAsync(order.BuildingId, cancellationToken);

            ApartmentResponseDTO? apartmentDTO = null;

            if (order.ApartmentId != null)
                apartmentDTO = await GetApartmentResponseDTOByApartmentIdAsync(order.ApartmentId.Value);

            var orderMaterials = await _orderRepository.GetOrderMaterialsByIdAsync(orderId, cancellationToken);

            var orderMaterialDTOs = orderMaterials == null ? null :
                orderMaterials.Select(om => new OrderMaterialResponseDTO(
                    om.Id,
                    om.OrderId,
                    new(
                        om.Material.Id,
                        om.Material.Name,
                        om.Material.Unit,
                        om.Material.Price),
                    om.Quantity)).ToList();

            return new OrderResponseDTO(
                order.Id,
                residentDTO,
                order.Description,
                new(
                    order.OrderCategory.Id,
                    order.OrderCategory.Category,
                    new(
                        order.OrderCategory.Type.Id,
                        order.OrderCategory.Type.Type)),
                buildingDTO,
                apartmentDTO,
                new(
                    order.Priority.Id,
                    order.Priority.Priority1),
                order.ContactPhone,
                order.ContactEmail,
                new(
                    order.Status.Id,
                    order.Status.Status),
                order.CreatedAt,
                orderMaterialDTOs);
        }
    }
}
