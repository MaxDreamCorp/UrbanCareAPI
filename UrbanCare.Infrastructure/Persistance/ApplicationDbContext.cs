using Microsoft.EntityFrameworkCore;
using UrbanCare.Domain.Entities;

namespace UrbanCare.Infrastructure.Persistance;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<BuildingType> BuildingTypes { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }

    public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

    public virtual DbSet<FloorMaterial> FloorMaterials { get; set; }

    public virtual DbSet<ManagementCompany> ManagementCompanies { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderCategory> OrderCategories { get; set; }

    public virtual DbSet<OrderExecutor> OrderExecutors { get; set; }

    public virtual DbSet<OrderMaterial> OrderMaterials { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<PassportDatum> PassportData { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<QualificationCategory> QualificationCategories { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Resident> Residents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SessionStatus> SessionStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPersonalDatum> UserPersonalData { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<WallMaterial> WallMaterials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("apartments");

            entity.HasIndex(e => e.BuildingId, "FK_apartment_building_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.Entrance).HasColumnName("entrance");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.RoomsCount).HasColumnName("rooms_count");

            entity.HasOne(d => d.Building).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_apartment_building");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("buildings");

            entity.HasIndex(e => e.BuildingTypeId, "FK_building_building_type_idx");

            entity.HasIndex(e => e.FloorMaterialId, "FK_building_floor_material_idx");

            entity.HasIndex(e => e.RegionId, "FK_building_region_idx");

            entity.HasIndex(e => e.WallMaterialId, "FK_building_wall_material_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BuildingTypeId).HasColumnName("building_type_id");
            entity.Property(e => e.FloorCount).HasColumnName("floor_count");
            entity.Property(e => e.FloorMaterialId).HasColumnName("floor_material_id");
            entity.Property(e => e.Number)
                .HasMaxLength(45)
                .HasColumnName("number");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.WallMaterialId).HasColumnName("wall_material_id");
            entity.Property(e => e.YearBuilt)
                .HasColumnType("year")
                .HasColumnName("year_built");

            entity.HasOne(d => d.BuildingType).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.BuildingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_building_building_type");

            entity.HasOne(d => d.FloorMaterial).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.FloorMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_building_floor_material");

            entity.HasOne(d => d.Region).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_building_region");

            entity.HasOne(d => d.WallMaterial).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.WallMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_building_wall_material");
        });

        modelBuilder.Entity<BuildingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("building_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(150)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("certifications");

            entity.HasIndex(e => e.EmployeeId, "FK_certification_employee_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.IssuingOrganization)
                .HasMaxLength(150)
                .HasColumnName("issuing_organization");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .HasColumnName("number");
            entity.Property(e => e.ScanFilePath)
                .HasMaxLength(255)
                .HasColumnName("scan_file_path");

            entity.HasOne(d => d.Employee).WithMany(p => p.Certifications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_certification_employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.EmployeePositionId, "FK_employee_employee_position_idx");

            entity.HasIndex(e => e.StatusId, "FK_employee_employee_status_idx");

            entity.HasIndex(e => e.ManagementCompanyId, "FK_employee_management_company_idx");

            entity.HasIndex(e => e.QualificationCategoryId, "FK_employee_qualification_category_idx");

            entity.HasIndex(e => e.UserId, "FK_employee_user_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EmployeePositionId).HasColumnName("employee_position_id");
            entity.Property(e => e.EmploymentDate).HasColumnName("employment_date");
            entity.Property(e => e.ExperienceYears).HasColumnName("experience_years");
            entity.Property(e => e.ManagementCompanyId).HasColumnName("management_company_id");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.QualificationCategoryId).HasColumnName("qualification_category_id");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.EmployeePosition).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeePositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employee_employee_position");

            entity.HasOne(d => d.ManagementCompany).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ManagementCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employee_management_company");

            entity.HasOne(d => d.QualificationCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.QualificationCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employee_qualification_category");

            entity.HasOne(d => d.Status).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employee_employee_status");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_employee_user");
        });

        modelBuilder.Entity<EmployeePosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee_positions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<EmployeeStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee_statuses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<FloorMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("floor_materials");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ManagementCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("management_companies");


            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("materials");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Unit)
                .HasMaxLength(150)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.ApartmentId, "FK_order_apartment_idx");

            entity.HasIndex(e => e.BuildingId, "FK_order_building_idx");

            entity.HasIndex(e => e.DispatcherId, "FK_order_dispatcher_idx");

            entity.HasIndex(e => e.OrderCategoryId, "FK_order_order_category_idx");

            entity.HasIndex(e => e.StatusId, "FK_order_order_status_idx");

            entity.HasIndex(e => e.PriorityId, "FK_order_priority_idx");

            entity.HasIndex(e => e.ResidentId, "FK_order_resident_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AcceptedAt)
                .HasMaxLength(6)
                .HasColumnName("accepted_at");
            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.ChangedAt)
                .HasMaxLength(6)
                .HasColumnName("changed_at");
            entity.Property(e => e.CompletedAt)
                .HasMaxLength(6)
                .HasColumnName("completed_at");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(150)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DispatcherId).HasColumnName("dispatcher_id");
            entity.Property(e => e.OrderCategoryId).HasColumnName("order_category_id");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ResidentId).HasColumnName("resident_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("FK_order_apartment");

            entity.HasOne(d => d.Building).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_building");

            entity.HasOne(d => d.Dispatcher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DispatcherId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_order_dispatcher");

            entity.HasOne(d => d.OrderCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_order_category");

            entity.HasOne(d => d.Priority).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_priority");

            entity.HasOne(d => d.Resident).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ResidentId)
                .HasConstraintName("FK_order_resident");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_order_status");
        });

        modelBuilder.Entity<OrderCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_categories");

            entity.HasIndex(e => e.TypeId, "FK_order_category_order_type_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(150)
                .HasColumnName("category");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.OrderCategories)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_category_order_type");
        });

        modelBuilder.Entity<OrderExecutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_executor");

            entity.HasIndex(e => e.ExecutorId, "FK_order_executor_executor_idx");

            entity.HasIndex(e => e.OrderId, "FK_order_executor_order_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExecutorId).HasColumnName("executor_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.Property(e => e.WorkPayment)
                .HasPrecision(10, 2)
                .HasColumnName("work_payment");

            entity.HasOne(d => d.Executor).WithMany(p => p.OrderExecutors)
                .HasForeignKey(d => d.ExecutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_executor_executor");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderExecutors)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_executor_order");
        });

        modelBuilder.Entity<OrderMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_materials");

            entity.HasIndex(e => e.MaterialId, "FK_order_materials_material_idx");

            entity.HasIndex(e => e.OrderId, "FK_order_materials_order_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.OrderMaterials)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_materials_material");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderMaterials)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_materials_order");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_statuses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<PassportDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passport_data");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Department)
                .HasMaxLength(500)
                .HasColumnName("department");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("department_code")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Number)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("number")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Seria)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("seria")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.OrderId, "FK_payment_order_idx");

            entity.HasIndex(e => e.PaymentMethodId, "FK_payment_payment_method_idx");


            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaidAt)
                .HasMaxLength(6)
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentCode)
                .HasMaxLength(255)
                .HasColumnName("payment_code");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_payment_order");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_payment_payment_method");

        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_methods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("priorities");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Priority1)
                .HasMaxLength(20)
                .HasColumnName("priority");
        });

        modelBuilder.Entity<QualificationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("qualification_categories");

            entity.HasIndex(e => e.Code, "code_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MinExperienceYears).HasColumnName("min_experience_years");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SalaryCoefficient).HasColumnName("salary_coefficient");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("regions");

            entity.HasIndex(e => e.ManagementCompanyId, "FK_management_company_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CommonAddress)
                .HasMaxLength(255)
                .HasColumnName("common_address");
            entity.Property(e => e.ManagementCompanyId).HasColumnName("management_company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.ManagementCompany).WithMany(p => p.Regions)
                .HasForeignKey(d => d.ManagementCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_management_company_id");
        });

        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("residents");

            entity.HasIndex(e => e.ApartmentId, "FK_resident_appartment_idx");

            entity.HasIndex(e => e.UserId, "FK_resident_user_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");
            entity.Property(e => e.IsLiving).HasColumnName("is_living");
            entity.Property(e => e.MovingIntoDate).HasColumnName("moving_into_date");
            entity.Property(e => e.MovingOutDate).HasColumnName("moving_out_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Residents)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resident_appartment");

            entity.HasOne(d => d.User).WithMany(p => p.Residents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_resident_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.AccessLevel, "access_level_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Role1, "role_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccessLevel)
                .HasMaxLength(2)
                .HasColumnName("access_level")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Role1)
                .HasMaxLength(40)
                .HasColumnName("role")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<SessionStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("session_statuses");

            entity.HasIndex(e => e.Status, "status_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleId, "FK_user_role_idx");

            entity.HasIndex(e => e.UserPersonalDataId, "FK_user_user_personal_data_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(32)
                .HasColumnName("password_salt");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserPersonalDataId).HasColumnName("user_personal_data_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role");

            entity.HasOne(d => d.UserPersonalData).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserPersonalDataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_user_personal_data");
        });

        modelBuilder.Entity<UserPersonalDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_personal_data");

            entity.HasIndex(e => e.PasportDataId, "FK_user_personal_data_pasport_data_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("inn")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PasportDataId).HasColumnName("pasport_data_id");
            entity.Property(e => e.Snils)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("snils")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.PasportData).WithMany(p => p.UserPersonalData)
                .HasForeignKey(d => d.PasportDataId)
                .HasConstraintName("FK_user_personal_data_pasport_data");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_sessions");

            entity.HasIndex(e => e.SessionStatusId, "FK_user_session_session_status_idx");

            entity.HasIndex(e => e.UserId, "FK_user_session_user_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DeviceInfo)
                .HasMaxLength(200)
                .HasColumnName("device_info");
            entity.Property(e => e.ExpiresAt)
                .HasMaxLength(6)
                .HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.LastActivityAt)
                .HasMaxLength(6)
                .HasColumnName("last_activity_at");
            entity.Property(e => e.LoginAt)
                .HasMaxLength(6)
                .HasColumnName("login_at");
            entity.Property(e => e.LogoutAt)
                .HasMaxLength(6)
                .HasColumnName("logout_at");
            entity.Property(e => e.SessionStatusId).HasColumnName("session_status_id");
            entity.Property(e => e.SessionToken)
                .HasMaxLength(255)
                .HasColumnName("session_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SessionStatus).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.SessionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_session_session_status");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_session_user");
        });

        modelBuilder.Entity<WallMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wall_materials");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
